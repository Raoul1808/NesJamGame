using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NesJamGame.Engine.Graphics
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2? position;
        public Vector2 offset;
        public float rotation, scale, layerDepth;
        public SpriteEffects flip;
        public Color color;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            position = null;
            offset = Vector2.Zero;
            rotation = 0f;
            scale = 1f;
            layerDepth = 0f;
            flip = SpriteEffects.None;
            color = Color.White;
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            offset = Vector2.Zero;
            rotation = 0f;
            scale = 1f;
            layerDepth = 0f;
            flip = SpriteEffects.None;
            color = Color.White;
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 offset, float rotation, float scale, SpriteEffects flip, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.offset = offset;
            this.rotation = rotation;
            this.scale = scale;
            layerDepth = 0f;
            this.flip = flip;
            this.color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, position == null ? Vector2.Zero : (Vector2)position);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, flip, layerDepth);
        }
    }
}
