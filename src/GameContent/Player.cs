using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Utilities;
using NesJamGame.GameContent.Scenes;

namespace NesJamGame.GameContent
{
    public class Player : IEntity
    {
        Sprite sprite;
        Vector2 pos;
        // Velocity in Pixels Per Frame (2*60 = 120 Pixels Per Seconds)
        // const float VELOCITY = 2f;
        const float VELOCITY = 120f;

        // Bullet delay in Frames: 10. Seconds: 0.1
        const double BULLET_SHOOT_DELAY = 0.1;

        double shootDelay;
        double speed;

        public Player(int x, int y)
        {
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["DevPlayer"],
                rectangle = new Rectangle(0, 0, 16, 16)
            };
            pos = new Vector2(x * 8, y * 8);
            shootDelay = 0;
            speed = 0;
        }

        public void Update()
        {
            float time = (float)GlobalTime.ElapsedGameMilliseconds / 1000;
            if (GameInput.IsButtonDown(NESInput.Up))
            {
                pos.Y -= VELOCITY * time;
                if (pos.Y < 0) pos.Y = 0;
            }
            if (GameInput.IsButtonDown(NESInput.Down))
            {
                pos.Y += VELOCITY * time;
                if (pos.Y + sprite.rectangle.Height > 240) pos.Y = 240 - sprite.rectangle.Height;
            }
            if (GameInput.IsButtonDown(NESInput.Left))
            {
                pos.X -= VELOCITY * time;
                if (pos.X < 0) pos.X = 0;
            }
            if (GameInput.IsButtonDown(NESInput.Right))
            {
                pos.X += VELOCITY * time;
                if (pos.X + sprite.rectangle.Width > 256) pos.X = 256 - sprite.rectangle.Width;
            }
            if (GameInput.IsButtonDown(NESInput.A))
            {
                if (shootDelay >= BULLET_SHOOT_DELAY)
                {
                    ShootBullet();
                }
            }

            if (GameInput.IsNewPress(NESInput.B))
            {
                speed = 0.8;
            }

            if (speed > 0)
            {
                GlobalTime.ChangeSpeed(Easing.ApplyEasingFromOne(1 - speed, EasingMode.CubicIn));
                speed -= (time*5);
            }
            else speed = 0;

            if (shootDelay < BULLET_SHOOT_DELAY)
                shootDelay += time;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Point((int)pos.X, (int)pos.Y));
        }

        public Vector2 GetPos()
        {
            return pos;
        }

        public bool CanDispose()
        {
            return false;
        }


        private void ShootBullet()
        {
            shootDelay = 0;
            GameScene.AddEntity(new Bullet(this, BulletPath.StraightUp, new Vector2(pos.X + 4, pos.Y + 2)));
            GameScene.AddEntity(new Bullet(this, BulletPath.StraightUp, new Vector2(pos.X + 10, pos.Y + 2)));
        }
    }
}
