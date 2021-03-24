using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;

namespace NesJamGame.GameContent
{
    // This class exists just for the SkyBackground class.
    public class SimpleMovingObject
    {
        public Texture2D texture;
        public Vector2 position;
        public float velocity;
        public float scale;

        public void Update()
        {
            position.Y += velocity * (float)GlobalTime.ElapsedGameMilliseconds / 1000;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2((int)position.X, (int)position.Y), null, Color.DimGray, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }
}
