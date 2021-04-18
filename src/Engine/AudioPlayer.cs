using NesJamGame.GameContent;

namespace NesJamGame.Engine
{
    public static class AudioPlayer
    {
        static double volume;
        public static int Volume
        {
            get
            {
                return (int)(volume * 10);
            }
            set
            {
                volume = (double)decimal.Divide(value, 10);
            }
        }

        public static void PlayAudio(string sound)
        {
            ContentIndex.Sounds[sound].Play((float)volume, 0f, 0f);
        }
    }
}
