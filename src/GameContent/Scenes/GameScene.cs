using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.GameContent.Entities;
using System.Collections.Generic;

namespace NesJamGame.GameContent.Scenes
{
    public class GameScene : IScene
    {
        public static List<Entity> entities;
        public static bool Flip;
        static List<Entity> toAdd;
        List<int> toRemove;

        const double FLIP_TIME = 0.333333333333333333333;
        double time;

        public GameScene()
        {
            entities = new List<Entity>();
            toAdd = new List<Entity>();
            entities.Add(new Player(15, 25));
            toRemove = new List<int>();

            entities.Add(new ClassicEnemy(2, 2));
            Flip = false;
            time = 0;
        }

        public void Update()
        {
            Flip = false;
            time += GlobalTime.ElapsedGameMilliseconds / 1000;
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
                entities.RemoveAt(toRemove[i] - i);
            }
            toRemove = new List<int>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Entity entity in entities)
            {
                entity.Draw(spriteBatch);
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
