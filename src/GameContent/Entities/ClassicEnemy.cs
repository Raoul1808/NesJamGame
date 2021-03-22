using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Utilities;
using NesJamGame.GameContent.Scenes;
using System;

namespace NesJamGame.GameContent.Entities
{
    public class ClassicEnemy : IEntity
    {
        const float SHOT_SPEED = 2f;
        const float X_VELOCITY = 60f;
        const float ENTER_TIME = 1;

        Sprite sprite;
        Vector2 position;
        double shootTime;
        bool goingLeft;
        bool canDispose;
        Random random;
        double progress;
        double appearTime;
        int dstYPos;
        int srcYPos;

        public ClassicEnemy(double appearTime, int yPos, int? xPos = null, bool left = true)
        {
            sprite = new Sprite()
            {
                texture = ContentIndex.Textures["DevEnemy"],
                rectangle = new Rectangle(0, 0, 16, 16)
            };
            random = new Random();
            position = new Vector2((xPos == null ? random.Next(0, 30) : (int)xPos) * 8, -16);
            goingLeft = left;
            canDispose = false;
            shootTime = 0;
            this.appearTime = appearTime;
            dstYPos = yPos * 8;
            srcYPos = (int)position.Y;
            progress = 0;
        }

        public void Update()
        {
            float time = (float)GlobalTime.ElapsedGameMilliseconds / 1000;

            if (progress < appearTime)
            {
                position.Y = (float)(srcYPos + (dstYPos - srcYPos) * Easing.ApplyEasingFromOne(progress/appearTime, EasingMode.CubicOut));
                progress += time;
            }

            if (goingLeft) position.X -= X_VELOCITY * time;
            else position.X += X_VELOCITY * time;
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
            if (shootTime >= (SHOT_SPEED - (random.NextDouble() + (random.Next(0, 2) - 1)/2)))
            {
                shootTime -= SHOT_SPEED;
                if (progress/appearTime >= 1) GameScene.AddEntity(new Bullet(this, BulletPath.StraightDown, new Vector2(position.X + 7, position.Y + 10), 50f));
            }
            shootTime += time;

            foreach (IEntity entity in GameScene.entities)
            {
                if (entity.GetType() == typeof(Bullet))
                {
                    Bullet ent = (Bullet)entity;
                    if (ent.entity.GetType() == typeof(Player))
                    {
                        if (ent.GetBbox().Intersects(GetBbox()))
                        {
                            canDispose = true;
                            ent.SendHit();
                        }
                    }
                }
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
            return canDispose;
        }

        public void SendHit()
        {

        }

        public Rectangle GetBbox()
        {
            return new Rectangle((int)position.X, (int)position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
        }
    }
}
