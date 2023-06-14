using Microsoft.Xna.Framework;
using NesJamGame.Engine.Utilities;

namespace NesJamGame.Engine
{
    // This class is here just to synchronize everything that is time-based in the game.
    // I am currently working on a time-based engine outside of this jam, but it's a mess.
    // Hopefully this code ends up doing better than the one in my other engine.
    // - Mew
    public static class GlobalTime
    {
        public static double TotalProgramMilliseconds;
        public static double ElapsedProgramMilliseconds;
        public static double TotalGameMilliseconds;
        public static double ElapsedGameMilliseconds;
        static double SpeedMultiplier;

        static EasingMode easingMode;
        static double speed;
        static double oSpeed;
        static double time;
        static double progress;
        static bool pulsing = false;

        public static void Initialize()
        {
            TotalProgramMilliseconds = 0;
            TotalGameMilliseconds = 0;
            ElapsedProgramMilliseconds = 0;
            ElapsedGameMilliseconds = 0;
            SpeedMultiplier = 1;
        }

        public static void Update(GameTime gameTime)
        {
            TotalProgramMilliseconds = gameTime.TotalGameTime.TotalMilliseconds;
            ElapsedProgramMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;

            if (pulsing)
            {
                if (progress > time)
                {
                    pulsing = false;
                    SpeedMultiplier = oSpeed;
                }
                else
                {
                    SpeedMultiplier = speed + (oSpeed - speed) * Easing.ApplyEasingFromOne(progress / time, easingMode);
                    progress += ElapsedProgramMilliseconds / 1000;
                }
            }
            
            ElapsedGameMilliseconds = ElapsedProgramMilliseconds * SpeedMultiplier;
            TotalGameMilliseconds += ElapsedGameMilliseconds;
        }

        public static void ChangeSpeed(double multiplier)
        {
            SpeedMultiplier = multiplier;
        }

        public static void TimePulse(double _speed, double _time, EasingMode _easing)
        {
            if (pulsing) return;
            speed = _speed;
            time = _time;
            easingMode = _easing;
            pulsing = true;
            progress = 0;
            oSpeed = SpeedMultiplier;
        }
    }
}
