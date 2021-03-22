using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NesJamGame.GameContent
{
    public static class ContentIndex
    {
        static List<string> textureNames;
        public static Dictionary<string, Texture2D> Textures;

        public static void LoadContent(ContentManager Content)
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
        }
    }
}
