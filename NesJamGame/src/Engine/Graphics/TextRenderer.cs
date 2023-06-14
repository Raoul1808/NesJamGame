using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace NesJamGame.Engine.Graphics
{
    public static class TextRenderer
    {
        static Texture2D Characters;
        static Dictionary<char, Rectangle> CharIndex;

        public static void Initialize(Texture2D characters, string chars)
        {
            Characters = characters;
            CharIndex = new Dictionary<char, Rectangle>(chars.Length);
            int index = 0;
            for (int i = 0; i < Characters.Height; i+=8)
            {
                for (int j = 0; j < Characters.Width; j+=8)
                {
                    CharIndex.Add(chars[index], new Rectangle(j, i, 8, 8));
                    index++;
                }
            }
        }

        public static void RenderText(SpriteBatch spriteBatch, string text, Point location)
        {
            List<Rectangle> rectangles = new List<Rectangle>(text.Length);
            foreach(char letter in text)
            {
                if (!CharIndex.ContainsKey(letter))
                {
                    rectangles.Add(new Rectangle(Characters.Width - 1, Characters.Height - 1, 1, 1));
                }
                else
                {
                    rectangles.Add(CharIndex[letter]);
                }
            }

            int y = location.Y;
            int x = location.X;
            for (int i = 1; i <= rectangles.Count; i++)
            {
                if (x > 31)
                {
                    x = 0;
                    y++;
                }
                spriteBatch.Draw(Characters, new Vector2(x++ * 8, y * 8), rectangles[i-1], Color.White);
            }
        }
    }
}
