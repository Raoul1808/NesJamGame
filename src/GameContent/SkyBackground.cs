using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using System;
using System.Collections.Generic;

namespace NesJamGame.GameContent
{
    public class SkyBackground
    {
        List<SimpleMovingObject> stars;
        List<int> toRemove;
        Random random;

        const float STAR_COOLDOWN = 0.1f;
        float cooldown = 0f;

        public SkyBackground()
        {
            stars = new List<SimpleMovingObject>();
            random = new Random();
            toRemove = new List<int>();
        }

        public void Update()
        {
            if (cooldown >= STAR_COOLDOWN)
            {
                float scale = random.Next(1, 7);
                scale = scale <= 3 ? 1 : scale <= 5 ? 2 : 3;
                stars.Add(new SimpleMovingObject()
                {
                    position = new Vector2(random.Next(0, 256), -1),
                    texture = ContentIndex.Pixel,
                    velocity = (float)(random.Next(5, 50) * 1.5 * scale),
                    scale = scale 
                });

                cooldown -= STAR_COOLDOWN;
            }
            cooldown += (float)GlobalTime.ElapsedGameMilliseconds / 1000;

            for(int i = 0; i < stars.Count; i++)
            {
                stars[i].Update();
                if (stars[i].position.Y >= 256) toRemove.Add(i);
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                stars.RemoveAt(toRemove[i] - i);
            }
            toRemove = new List<int>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(SimpleMovingObject star in stars)
            {
                star.Draw(spriteBatch);
            }
        }
    }
}
