using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;

namespace NesJamGame.GameContent.Scenes
{
    public class StartScene : IScene
    {
        double time;
        string text;

        public StartScene()
        {
            time = 0;
            text = "";
        }

        public void Update()
        {
            time += GlobalTime.ElapsedGameMilliseconds / 1000;

            if (time >= 1 && text == "")
            {
                text = "RAOUL1808";
                ContentIndex.Sounds["splash"].Play();
            }
            if (time >= 2.5)
            {
                SceneManager.ChangeScene("MenuScene");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            TextRenderer.RenderText(spriteBatch, text, new Point(11, 15));
        }
    }
}
