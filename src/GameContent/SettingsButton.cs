using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Input;

namespace NesJamGame.GameContent
{
    public class SettingsButton : Button
    {
        Sprite cog;
        Sprite frame;

        double time;
        double pos;

        public SettingsButton()
        {
            rectangle = new Rectangle(256 * Program.CanvasScale - 32 * Program.CanvasScale, 0, 32 * Program.CanvasScale, 32 * Program.CanvasScale);
            cog = new Sprite()
            {
                texture = ContentIndex.Textures["cog_256"],
                rectangle = new Rectangle(256 * Program.CanvasScale - 16 * Program.CanvasScale, 16 * Program.CanvasScale, 32 * Program.CanvasScale, 32 * Program.CanvasScale),
            };
            cog.offset = new Vector2(cog.texture.Width / 2, cog.texture.Height / 2);
            frame = new Sprite()
            {
                texture = ContentIndex.Textures["frame_256"],
                rectangle = rectangle
            };
            pos = 0;
        }

        public override void Update()
        {
            time += GlobalTime.ElapsedProgramMilliseconds / 1000;
            if (time >= 3 && cog.rectangle.Y >= -16 * Program.CanvasScale && frame.rectangle.Y >= -32 * Program.CanvasScale)
            {
                pos += 1/3;
                if (pos >= 1 / Program.CanvasScale)
                {
                    pos = 0;
                    cog.rectangle.Y--;
                    frame.rectangle.Y--;
                }
            }
            if (InputManager.MouseStateChanged())
            {
                cog.rectangle.Y = 16 * Program.CanvasScale;
                frame.rectangle.Y = 0;
                time = 0;
            }
            base.Update();
        }

        public override void OnClickHeld()
        {
            time = 0;
            cog.rotation += 0.05f;
            cog.color = Color.Gray;
            frame.color = Color.Gray;
        }

        public override void OnHover()
        {
            time = 0;
            cog.rotation += 0.05f;
            cog.color = Color.LightGray;
            frame.color = Color.LightGray;
        }

        public override void OnIdle()
        {
            cog.color = Color.White;
            frame.color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            frame.Draw(spriteBatch);
            cog.Draw(spriteBatch);
        }
    }
}
