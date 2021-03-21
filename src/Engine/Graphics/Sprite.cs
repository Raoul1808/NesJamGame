using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NesJamGame.Engine.Graphics
{
    public class Sprite
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 offset;
        public float rotation;
        public Color color;
        public SpriteEffects flip;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, null, color, rotation, offset, flip, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Point point)
        {
            spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, rectangle.Width, rectangle.Height), null, color, rotation, offset, flip, 0f);
        }
    }
}
