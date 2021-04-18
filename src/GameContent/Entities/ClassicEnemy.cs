using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Utilities;
using NesJamGame.GameContent.Scenes;
using System;

namespace NesJamGame.GameContent.Entities
{
    public class ClassicEnemy : Entity
    {
        const float X_VELOCITY = 60f;

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

        public ClassicEnemy(double appearTime, int yPos, int? xPos = null, bool moving = true, bool left = true)
        {
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["Enemies/ClassicEnemy"],
                rectangle = new Rectangle(0, 0, 16, 16),
            };
            random = new Random();
            position = new Vector2((xPos == null ? random.Next(0, 30) : (int)xPos) * 8, -16);
            goingLeft = left;
            canDispose = false;
            this.appearTime = appearTime;
            dstYPos = yPos * 8;
            srcYPos = (int)position.Y;
            progress = 0;
            this.moving = moving;
        }

        public override void Update()
        {
            float time = (float)GlobalTime.ElapsedGameMilliseconds / 1000;

            if (progress < appearTime)
            {
                position.Y = (float)(srcYPos + (dstYPos - srcYPos) * Easing.ApplyEasingFromOne(progress/appearTime, EasingMode.CubicOut));
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

            if (GameScene.Flip)
            {
                sprite.flip = sprite.flip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }

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
            AudioPlayer.PlayAudio($"hit{random.Next(1, 5)}");
            canDispose = true;
        }

        public override Rectangle GetBbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
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
