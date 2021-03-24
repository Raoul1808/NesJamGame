using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.GameContent.Scenes;

namespace NesJamGame.GameContent.Entities
{
    public abstract class Entity
    {
        public virtual void Update()
        {
            foreach (Entity entity in GameScene.entities)
            {
                if (entity.GetBbox().Intersects(GetBbox()))
                {
                    OnEntityCollision(entity);
                }
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void OnEntityCollision(Entity entity);
        public abstract Rectangle GetBbox();
        public abstract Vector2 GetPos();
        public abstract bool CanDispose();
        public abstract void SendHit();
    }
}
