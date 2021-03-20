using Microsoft.Xna.Framework;

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
            ElapsedGameMilliseconds = ElapsedProgramMilliseconds * SpeedMultiplier;
            TotalGameMilliseconds += ElapsedGameMilliseconds;
        }

        public static void ChangeSpeed(double multiplier)
        {
            SpeedMultiplier = multiplier;
        }
    }
}
