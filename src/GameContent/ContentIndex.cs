using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NesJamGame.GameContent
{
    public static class ContentIndex
    {
        static List<string> textureNames;
        public static Dictionary<string, Texture2D> Textures;
        public static Texture2D Pixel;

        public static void LoadContent(ContentManager Content, GraphicsDevice GD)
        {
            textureNames = new List<string>()
            {
                "chars",
                "DevPlayer",
                "DevBullet",
                "DevEnemy"
            };

            Textures = new Dictionary<string, Texture2D>();
            foreach(string asset in textureNames)
            {
                Textures.Add(asset, Content.Load<Texture2D>(asset));
            }
            Pixel = new Texture2D(GD, 1, 1);
            Pixel.SetData(new Color[] { Color.White });
        }
    }
}
