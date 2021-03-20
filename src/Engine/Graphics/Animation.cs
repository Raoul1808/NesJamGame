using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NesJamGame.Engine.Graphics
{
    public class Animation
    {
        List<Sprite> sprites;
        List<double> durations;
        int currentFrame;
        double internalTime;
        bool looping;
        public bool AnimationDone;

        public Animation(Sprite[] sprites, double duration, bool looping = true)
        {
            this.sprites = new List<Sprite>(sprites);
            durations = new List<double>();
            for (int i = 0; i < sprites.Length; i++)
            {
                durations.Add(duration);
            }
            currentFrame = 0;
            internalTime = 0;
            this.looping = looping;
            AnimationDone = false;
        }

        public Animation(Sprite[] sprites, double[] durations, bool looping = true)
        {
            this.sprites = new List<Sprite>(sprites);
            this.durations = new List<double>(durations);
            currentFrame = 0;
            internalTime = 0;
            this.looping = looping;
            AnimationDone = false;
        }

        public void Update()
        {
            if (AnimationDone) return;
            if (internalTime >= durations[currentFrame])
            {
                internalTime -= durations[currentFrame];
                currentFrame++;
                if (currentFrame >= sprites.Count)
                {
                    if (looping) currentFrame = 0;
                    else
                    {
                        AnimationDone = true;
                        currentFrame--;
                        return;
                    }
                }
            }
            internalTime += GlobalTime.ElapsedGameMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            sprites[currentFrame].Draw(spriteBatch, position);
        }
    }
}
