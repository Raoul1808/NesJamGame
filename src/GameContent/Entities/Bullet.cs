using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;

namespace NesJamGame.GameContent.Entities
{
    public class Bullet : Entity
    {
        public Entity entity;
        Sprite sprite;
        BulletPath path;
        Vector2 position;

        float bulletSpeed;
        bool canDispose;

        public Bullet(Entity entity, BulletPath path, Vector2 position, float bulletSpeed)
        {
            this.entity = entity;
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["DevBullet"],
                rectangle = new Rectangle(0, 0, 2, 4)
            };
            this.position = position;
            this.bulletSpeed = bulletSpeed;
            canDispose = false;
            this.path = path;
        }

        public override void Update()
        {
            float time = (float)GlobalTime.ElapsedGameMilliseconds / 1000;
            switch (path)
            {
                case BulletPath.StraightUp:
                    position.Y -= bulletSpeed * time;
                    break;

                case BulletPath.StraightDown:
                    position.Y += bulletSpeed * time;
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Point((int)position.X, (int)position.Y));
        }

        public override Vector2 GetPos()
        {
            return position;
        }

        public override bool CanDispose()
        {
            return position.Y < 0 || position.Y > 240 || canDispose;
        }

        public override void SendHit()
        {
            canDispose = true;
        }

        public override Rectangle GetBbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
        }

        public override void OnEntityCollision(Entity entity)
        {
            
        }
    }
}
