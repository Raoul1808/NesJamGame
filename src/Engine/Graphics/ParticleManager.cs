using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NesJamGame.Engine.Graphics
{
    public static class ParticleManager
    {
        static List<Particle[]> particles;
        static List<int> toRemove;
        static Random random;

        public static void CreateParticles(Texture2D texture, Vector2 startingPosition, int amount, Vector2 direction, float spread, float minSpeed, float maxSpeed, Color color, float colorHueShift, double timeLeft)
        {
            if (particles == null) particles = new List<Particle[]>();
            Particle[] _particles = new Particle[amount];
            random = new Random();
            double angle = random.NextDouble() * (Math.PI * 4);
                
            for (int i = 0; i < amount; i++)
            {
                // NOTE: I'm not very good at trigonometry, so enjoy a code that's messy but works
                Vector2 defDirection = new Vector2((float)(Math.Cos(angle) * random.NextDouble()), (float)(Math.Sin(angle) * random.NextDouble()));
                if (direction != Vector2.Zero)
                {
                    defDirection = direction;
                    var angle1 = random.NextDouble() * ((Math.PI / 180) * spread);
                    defDirection.Y += (float)(Math.Sin(angle1) * ((random.NextDouble() * 2) - 1));
                    defDirection.X += (float)(Math.Cos(angle1) * ((random.NextDouble() * 2) - 1));
                }

                HSLColor col = HSLColor.FromColor(color);
                col.H += (float)(random.Next((int)(-colorHueShift*1000), (int)(colorHueShift*1000))/1000);

                float sp = random.Next((int)(minSpeed * 1000), (int)(maxSpeed * 1000)) / 1000;

                _particles[i] = new Particle()
                {
                    texture = texture,
                    position = startingPosition,
                    velocity = defDirection,
                    color = col.ToRgbColor(),
                    speed = sp,
                    alpha = 1.0f,
                };
            }

            particles.Add(_particles);
        }

        public static void UpdateParticles()
        {
            if (particles == null) return;
            toRemove = new List<int>();
            float lastUpdate = (float)GlobalTime.ElapsedGameMilliseconds / 1000;
            for (int i = 0; i < particles.Count; i++)
            {
                bool canRemove = true;
                foreach(Particle particle in particles[i])
                {
                    particle.position += particle.velocity * particle.speed * lastUpdate;
                    if (particle.timeLeft > 0) particle.timeLeft -= lastUpdate;
                    else { particle.alpha -= (float)(1 * lastUpdate); }
                    if (particle.alpha <= 0) continue;
                    canRemove = false;
                }
                if (canRemove) toRemove.Add(i);
            }

            for (int i = 0; i < toRemove.Count; i++)
            {
                particles.RemoveAt(toRemove[i] - i);
            }
        }

        public static void DrawParticles(SpriteBatch spriteBatch)
        {
            if (particles == null) return;
            foreach(Particle[] _particles in particles)
            {
                foreach(Particle particle in _particles)
                {
                    if (particle.position.X < 0 || particle.position.X > 256 || particle.position.Y < 0 || particle.position.Y > 240) continue;
                    spriteBatch.Draw(particle.texture, new Vector2((int)particle.position.X, (int)particle.position.Y), particle.color * particle.alpha);
                }
            }
        }
    }

    internal class Particle
    {
        public Texture2D texture;
        public float speed;
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public float timeLeft;
        public float alpha;
    }
}
