using Microsoft.Xna.Framework.Graphics;

namespace NesJamGame.Engine
{
    public interface IScene
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }
}
