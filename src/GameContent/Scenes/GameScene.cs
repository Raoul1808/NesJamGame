﻿using Microsoft.Xna.Framework;
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
        static List<Entity> toAdd;
        List<int> toRemove;

        public GameScene()
        {
            entities = new List<Entity>();
            toAdd = new List<Entity>();
            entities.Add(new Player(15, 25));
            toRemove = new List<int>();

            entities.Add(new ClassicEnemy(2, 2));
        }

        public void Update()
        {
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
