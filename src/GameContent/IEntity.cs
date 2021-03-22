using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NesJamGame.GameContent
{
    public interface IEntity
    {
        void Update();
        void Draw(SpriteBatch spriteBatch);
        Vector2 GetPos();
        bool CanDispose();
    }
}
