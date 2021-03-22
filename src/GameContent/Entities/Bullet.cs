using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;

namespace NesJamGame.GameContent.Entities
{
    public class Bullet : IEntity
    {
        public IEntity entity;
        Sprite sprite;
        BulletPath path;
        Vector2 position;

        // Pixels Per Frame speed: 5f. Pixels Per Seconds: 300f
        const float STRAIGHT_VELOCITY = 300f;


        public Bullet(IEntity entity, BulletPath path)
        {
            this.entity = entity;
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["DevBullet"],
                rectangle = new Rectangle(0, 0, 2, 4)
            };
            position = entity.GetPos();
        }

        public Bullet(IEntity entity, BulletPath path, Vector2 position)
        {
            this.entity = entity;
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["DevBullet"],
                rectangle = new Rectangle(0, 0, 2, 4)
            };
            this.position = position;
        }

        public void Update()
        {
            float time = (float)GlobalTime.ElapsedGameMilliseconds / 1000;
            switch (path)
            {
                case BulletPath.StraightUp:
                    position.Y -= STRAIGHT_VELOCITY * time;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Point((int)position.X, (int)position.Y));
        }

        public Vector2 GetPos()
        {
            return position;
        }

        public bool CanDispose()
        {
            return position.Y < 0 || position.Y > 240;
        }
    }
}
