using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.IO;
using NesJamGame.GameContent.Entities;
using System;
using System.Collections.Generic;

namespace NesJamGame.GameContent.Scenes
{
    public class GameScene : IScene
    {
        public static List<Entity> entities;
        public static bool Flip;
        static List<Entity> toAdd;
        List<int> toRemove;
        Sprite goSpr;
        bool triggerOnce = false;
        int score;
        string highscoreText = "";
        int x = 5;

        const double FLIP_TIME = 0.333333333333333333333;
        double time;

        bool paused = false;
        int cursor;
        public static bool GameOver = false;

        // Spawning logic code stuff
        const double MAX_FORMATION_RATE = 0.333333333333;
        const double MAX_ZOOMING_RATE = 0.333333333333;
        const double SPAWN_LIMIT_PER_SECOND_MAX = 0.1;
        double currentSpawnLimit;
        double formationRate;
        double zoomingRate;
        double spawnTime;
        Random random;

        public GameScene()
        {
            EnemySpawner.Initialize();
            entities = new List<Entity>();
            toAdd = new List<Entity>();
            entities.Add(new Player(15, 25));
            toRemove = new List<int>();

            entities.Add(new ClassicEnemy(2, 2));
            Flip = false;
            time = 0;
            cursor = 13;

            goSpr = new Sprite()
            {
                texture = ContentIndex.Textures["GameOver"],
                rectangle = new Rectangle(70, 30, 104, 62),
            };
            score = 0;

            random = new Random();
            formationRate = 0;
            zoomingRate = 0;
            currentSpawnLimit = 1;
            spawnTime = 0;
        }

        public void Update()
        {
            time += GlobalTime.ElapsedGameMilliseconds / 1000;
            if (GameOver)
            {
                if (!triggerOnce)
                {
                    int highscore = Convert.ToInt32(SaveManager.GetValue("highscore"));
                    if (score > highscore)
                    {
                        highscoreText = "NEW HIGHSCORE!";
                        SaveManager.SetValue("highscore", score.ToString());
                        SaveManager.SaveJson();
                        x = 9;
                    }
                    else
                    {
                        highscoreText = "HIGHSCORE - " + highscore.ToString();
                    }
                    triggerOnce = true;
                }
                if (GameInput.IsNewPress(NESInput.A)) { SceneManager.ChangeScene("MenuScene"); }
            }
            if (paused && !GameOver)
            {
                if (GameInput.IsNewPress(NESInput.Down) && cursor < 14) { ContentIndex.Sounds["select"].Play(); cursor++; }
                if (GameInput.IsNewPress(NESInput.Up) && cursor > 13) { ContentIndex.Sounds["select"].Play(); cursor--; }
                if (GameInput.IsNewPress(NESInput.A) && cursor == 13) { ContentIndex.Sounds["selectPlay"].Play(); paused = false; }
                if (GameInput.IsNewPress(NESInput.A) && cursor == 14) { ContentIndex.Sounds["selectHit"].Play(); SceneManager.ChangeScene("MenuScene"); }
                return;
            }
            Flip = false;
            if (time >= FLIP_TIME)
            {
                Flip = true;
                time -= FLIP_TIME;
            }

            if (entities.Count < 500) TrySpawnEnemies();

            entities.AddRange(toAdd);
            toAdd = new List<Entity>();
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].CanDispose()) toRemove.Add(i);
                else entities[i].Update();
            }
            for (int i = 0; i < toRemove.Count; i++)
            {
                Entity e = entities[toRemove[i] - i];
                if (e.GetType() == typeof(ClassicEnemy) || e.GetType() == typeof(ShieldEnemy) || e.GetType() == typeof(ShootingEnemy)) score++;
                entities.RemoveAt(toRemove[i] - i);
            }
            toRemove = new List<int>();

            if (GameInput.IsNewPress(NESInput.Start)) paused = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity entity in entities)
            {
                entity.Draw(spriteBatch);
            }

            if (paused)
            {
                spriteBatch.Draw(ContentIndex.Pixel, new Rectangle(0, 0, 256, 240), null, Color.Black * 0.5f, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                TextRenderer.RenderText(spriteBatch, "PAUSED!", new Point(12, 10));
                TextRenderer.RenderText(spriteBatch, "RESUME", new Point(10, 13));
                TextRenderer.RenderText(spriteBatch, "MAIN MENU", new Point(10, 14));
                TextRenderer.RenderText(spriteBatch, ">", new Point(8, cursor));
            }

            if (GameOver)
            {
                goSpr.Draw(spriteBatch);
                TextRenderer.RenderText(spriteBatch, "SCORE - " + score.ToString(), new Point(9, 15));
                TextRenderer.RenderText(spriteBatch, highscoreText, new Point(x, 16));
                TextRenderer.RenderText(spriteBatch, "> MAIN MENU", new Point(8, 19));
            }
        }

        public static void AddEntity(Entity entity)
        {
            toAdd.Add(entity);
        }

        private void TrySpawnEnemies()
        {
            // Spawning logic:
            // The game should start out pretty slow with random enemies coming from above and few formations making up.
            // Then the game will start spamming shooting enemies protected by shielded enemies.
            // Shooting enemies should shoot a bit faster over time.
            // At some point, enemies that go accross the screen should start appearing. The player should avoid those.
            // Finally, have a hard enemy cap otherwise the game will lag from too many enemies and bullets.
            //
            // Math spawning logic:
            // --------------------
            // Game start:
            // Zooming Enemies rate: 0%
            // Formation rate: 0.1%
            // --------------------
            // Rate increase:
            // Formation rate: 0.1% per second, 0.2% when above 10%, caps at 33%
            // Zooming Enemies rate: 0% per second, 0.1% when formation rate is at 10%, caps at 33%

            double time = GlobalTime.ElapsedGameMilliseconds / 1000;

            if (formationRate < MAX_FORMATION_RATE) formationRate += (formationRate > 10 ? 0.2 : 0.1) * time;
            if (formationRate > 10 && zoomingRate < MAX_ZOOMING_RATE) zoomingRate += 0.1 * time;
            spawnTime += time;
            if (spawnTime > currentSpawnLimit)
            {
                spawnTime -= currentSpawnLimit;
                if (currentSpawnLimit > SPAWN_LIMIT_PER_SECOND_MAX) currentSpawnLimit -= 0.0001;
                double rng = random.NextDouble();
                if (rng < formationRate)
                {
                    EnemySpawner.SpawnFormation();
                }
                else if (rng > (1-zoomingRate))
                {
                    EnemySpawner.SpawnZooming(random.Next(0, 32), 100/score);
                }
                else
                {
                    EnemySpawner.SpawnNonSpecial(random.Next(0, 8), random.Next(0, 3));
                }
            }
        }
    }
}
