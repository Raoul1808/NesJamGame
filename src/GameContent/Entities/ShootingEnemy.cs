using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Utilities;
using NesJamGame.GameContent.Scenes;
using System;

namespace NesJamGame.GameContent.Entities
{
    public class ShootingEnemy : Entity
    {
        const float X_VELOCITY = 60f;
        const float BULLET_VELOCITY = 100f;
        const float SHOOT_TIME = 3;

        Sprite sprite;
        Vector2 position;
        bool goingLeft;
        bool canDispose;
        Random random;
        double progress;
        double appearTime;
        int dstYPos;
        int srcYPos;
        bool moving;
        double shootTime;
        Rectangle bbox;

        public ShootingEnemy(double randomSeed, double appearTime, int yPos, int? xPos = null, bool moving = true, bool left = true)
        {
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["Enemies/ShootingEnemy"],
                rectangle = new Rectangle(0, 0, 16, 16),
            };
            random = new Random((int)(randomSeed*10000));
            position = new Vector2((xPos == null ? random.Next(0, 30) : (int)xPos) * 8, -16);
            goingLeft = left;
            canDispose = false;
            this.appearTime = appearTime;
            dstYPos = yPos * 8;
            srcYPos = (int)position.Y;
            progress = 0;
            this.moving = moving;
            bbox = new Rectangle((int)position.X, (int)position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
        }

        public override void Update()
        {
            float time = (float)GlobalTime.ElapsedGameMilliseconds / 1000;

            if (progress < appearTime)
            {
                position.Y = (float)(srcYPos + (dstYPos - srcYPos) * Easing.ApplyEasingFromOne(progress / appearTime, EasingMode.CubicOut));
                progress += time;
            }

            if (moving)
            {
                if (goingLeft) position.X -= X_VELOCITY * time;
                else position.X += X_VELOCITY * time;
            }
            if (position.X < 0)
            {
                position.X = 0;
                goingLeft = false;
            }
            if (position.X + sprite.rectangle.Width > 256)
            {
                position.X = 256 - sprite.rectangle.Width;
                goingLeft = true;
            }

            if (shootTime > SHOOT_TIME)
            {
                shootTime -= SHOOT_TIME;
                GameScene.AddEntity(new Bullet(this, BulletPath.StraightDown, new Vector2(position.X + 7, position.Y + 14), BULLET_VELOCITY));
                shootTime += random.NextDouble() * SHOOT_TIME / 2;
            }
            shootTime += time;

            if (GameScene.Flip)
            {
                sprite.flip = sprite.flip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }

            bbox.X = (int)position.X;
            bbox.Y = (int)position.Y;

            base.Update();
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
            return canDispose || position.Y > 256;
        }

        public override void SendHit()
        {
            AudioPlayer.PlayAudio($"hit{random.Next(1, 5)}", 0f, (float)((2 * (position.X + sprite.rectangle.Width / 2) / 256) - 1));
            canDispose = true;
        }

        public override Rectangle GetBbox()
        {
            return bbox;
        }

        public override void OnEntityCollision(Entity entity)
        {
            if (entity.GetType() == typeof(Bullet))
                if (((Bullet)entity).entity.GetType() == typeof(Player) && !((Bullet)entity).CanDispose() && !canDispose)
                {
                    SendHit();
                    ((Bullet)entity).SendHit();
                }
        }
    }
}
