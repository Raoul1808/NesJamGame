using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.IO;
using NesJamGame.Engine.Utilities;
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

            //TextRenderer.RenderText(spriteBatch, "SCORE", new Point(0, 0));
            //TextRenderer.RenderText(spriteBatch, "00000000", new Point(0, 1));
            //TextRenderer.RenderText(spriteBatch, "HI-SCORE", new Point(24, 0));
            //TextRenderer.RenderText(spriteBatch, "00000000", new Point(24, 1));
        }

        public static void AddEntity(Entity entity)
        {
            toAdd.Add(entity);
        }
    }
}
