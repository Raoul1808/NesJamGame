using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Utilities;
using NesJamGame.GameContent.Scenes;
using System;

namespace NesJamGame.GameContent.Entities
{
    public class Player : Entity
    {
        Sprite sprite;
        Vector2 pos;
        // Velocity in Pixels Per Frame (2*60 = 120 Pixels Per Seconds)
        // const float VELOCITY = 2f;
        const float VELOCITY = 100f;

        // Bullet delay in Frames: 10. Seconds: 0.1
        const double BULLET_SHOOT_DELAY = 0.166666666666;

        // Pixels Per Frame speed: 5f. Pixels Per Seconds: 300f
        const float BULLET_SPEED = 300f;

        double shootDelay;
        double speed;

        Random random;

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
            random = new Random();
        }

        public override void Update()
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

            if (speed > 0)
            {
                GlobalTime.ChangeSpeed(Easing.ApplyEasingFromOne(1 - speed, EasingMode.CubicIn));
                speed -= (time*5);
            }
            else speed = 0;

            if (shootDelay < BULLET_SHOOT_DELAY)
                shootDelay += time;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, new Point((int)pos.X, (int)pos.Y));
        }

        public override Vector2 GetPos()
        {
            return pos;
        }

        public override bool CanDispose()
        {
            return false;
        }


        private void ShootBullet()
        {
            shootDelay = 0;
            GameScene.AddEntity(new Bullet(this, BulletPath.StraightUp, new Vector2(pos.X + 4, pos.Y + 2), BULLET_SPEED));
            GameScene.AddEntity(new Bullet(this, BulletPath.StraightUp, new Vector2(pos.X + 10, pos.Y + 2), BULLET_SPEED));
        }

        public override void SendHit()
        {

        }

        public override Rectangle GetBbox()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, sprite.rectangle.Width, sprite.rectangle.Height);
        }

        public override void OnEntityCollision(Entity entity)
        {
            if (entity.GetType() == typeof(Bullet))
            {
                if (((Bullet)entity).entity.GetType() != typeof(Player))
                {
                    ((Bullet)entity).entity.SendHit();
                    SendHit();
                }
            }
            else SendHit();
        }
    }
}
