using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;

namespace NesJamGame.GameContent
{
    public class DevScene : IScene
    {
        public DevScene(ContentManager Content)
        {

        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextRenderer.RenderText(spriteBatch, "HELLO WORLD", new Point(0, 10));
        }
    }
}
