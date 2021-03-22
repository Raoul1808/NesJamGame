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
                "HUD/cog_256",
                "HUD/frame_256",
                "HUD/Checkbox",
                "HUD/Checkmark",
                "HUD/ExitText",
                "HUD/Number2",
                "HUD/Number3",
                "HUD/Number4",
                "HUD/Number5",
                "HUD/Number6",
                "HUD/ResumeText",
                "HUD/WindowShakeText",
                "HUD/WindowSizeText",
                "HUD/LeftArrow",
                "HUD/RightArrow"
            };

            Textures = new Dictionary<string, Texture2D>();
            foreach(string asset in textureNames)
            {
                Textures.Add(asset, Content.Load<Texture2D>(asset));
            }
        }
    }
}
