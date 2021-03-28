using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Utilities;

namespace NesJamGame.GameContent.Scenes
{
    public class MenuScene : IScene
    {
        Sprite title;
        Vector2 position;
        double time;
        bool up;

        int cursor;

        const double BOB_TIME = 2;

        public MenuScene()
        {
            title = new Sprite()
            {
                texture = ContentIndex.Textures["SpaceExplorer"],
                rectangle = new Rectangle(0, 0, 71 * 2, 36 * 2),
            };
            position = new Vector2(60, 30);
            up = false;
            cursor = 20;
        }

        public void Update()
        {
            time += GlobalTime.ElapsedGameMilliseconds / 1000;
            double progress = time / BOB_TIME;
            if (progress > 1) progress = 1;
            position.Y = (float)((up ? 38 : 30) + (up ? -8 : 8) * Easing.ApplyEasingFromOne(progress, EasingMode.QuadInOut));
            if (progress == 1)
            {
                up = !up;
                time = 0;
            }
            if (GameInput.IsNewPress(NESInput.Down) && cursor < 22) { cursor++; ContentIndex.Sounds["select"].Play(); }
            if (GameInput.IsNewPress(NESInput.Up) && cursor > 20) { cursor--; ContentIndex.Sounds["select"].Play(); }
            if (GameInput.IsNewPress(NESInput.Left) && cursor == 21) { ChangeCanvasScale(true); ContentIndex.Sounds["selectHit"].Play(); }
            if (GameInput.IsNewPress(NESInput.Right) && cursor == 21) { ChangeCanvasScale(false); ContentIndex.Sounds["selectHit"].Play(); }
            if (GameInput.IsNewPress(NESInput.A) && cursor == 22) { ContentIndex.Sounds["selectHit"].Play(); Program.Quit(); }
            if (GameInput.IsNewPress(NESInput.A) && cursor == 20) { ContentIndex.Sounds["selectPlay"].Play(); ContentIndex.Sounds["selectHit"].Play(); SceneManager.ChangeScene("GameScene"); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            title.Draw(spriteBatch, new Point((int)position.X, (int)position.Y));
            TextRenderer.RenderText(spriteBatch, "PLAY", new Point(8, 20));
            TextRenderer.RenderText(spriteBatch, $"WINDOW SCALE  < {Program.CanvasScale} >", new Point(8, 21));
            TextRenderer.RenderText(spriteBatch, "EXIT", new Point(8, 22));
            TextRenderer.RenderText(spriteBatch, ">", new Point(6, cursor));
        }

        void ChangeCanvasScale(bool minus)
        {
            int scale = Program.CanvasScale;
            if (minus && scale > 2) scale--;
            else if (!minus && ((scale + 1) * 256 < Program.ScreenWidth) && ((scale + 1) * 240 < Program.ScreenHeight)) scale++;
            Program.UpdateCanvasScale(scale);
        }
    }
}
