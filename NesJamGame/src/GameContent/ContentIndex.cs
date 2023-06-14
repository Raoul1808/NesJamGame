using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NesJamGame.GameContent
{
    public static class ContentIndex
    {
        static List<string> textureNames;
        static List<string> soundNames;
        public static Dictionary<string, Texture2D> Textures;
        public static Dictionary<string, SoundEffect> Sounds;
        public static Texture2D Pixel;

        public static void LoadContent(ContentManager Content, GraphicsDevice GD)
        {
            textureNames = new List<string>()
            {
                "chars",
                "DevBullet",
                "PlayerShip",
                "GameOver",
                "SpaceExplorer",
                "Enemies/ClassicEnemy",
                "Enemies/ShieldEnemy",
                "Enemies/ShieldEnemyNoShield",
                "Enemies/ShootingEnemy"
            };

            soundNames = new List<string>()
            {
                "splash",
                "select",
                "selectHit",
                "selectPlay",
                "shoot",
                "hit1",
                "hit2",
                "hit3",
                "hit4",
                "death1",
                "death2",
                "death3",
            };

            Textures = new Dictionary<string, Texture2D>();
            Sounds = new Dictionary<string, SoundEffect>();
            foreach(string asset in textureNames)
            {
                Textures.Add(asset, Content.Load<Texture2D>(asset));
            }
            foreach (string asset in soundNames)
            {
                Sounds.Add(asset, Content.Load<SoundEffect>(asset));
            }
            Pixel = new Texture2D(GD, 1, 1);
            Pixel.SetData(new Color[] { Color.White });
        }
    }
}
