using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NesJamGame.Engine;
using NesJamGame.Engine.Input;
using System.Collections.Generic;

namespace NesJamGame.GameContent.Scenes
{
    public class GameScene : IScene
    {
        List<IEntity> entities;
        static List<IEntity> toAdd;
        List<int> toRemove;

        public GameScene()
        {
            entities = new List<IEntity>();
            toAdd = new List<IEntity>();
            entities.Add(new Player(15, 25));
            toRemove = new List<int>();
        }

        public void Update()
        {
            entities.AddRange(toAdd);
            toAdd = new List<IEntity>();
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
            foreach(IEntity entity in entities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public static void AddEntity(IEntity entity)
        {
            toAdd.Add(entity);
        }
    }
}
