using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace NesJamGame
{
    public class Program: Game
    {
        static void Main(string[] args)
        {
            using (Game game = new Program())
            {
                game.Run();
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RenderTarget2D Canvas;
        int CanvasScale = 1;

        public Program()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 256 * CanvasScale;
            graphics.PreferredBackBufferHeight = 240 * CanvasScale;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Canvas = new RenderTarget2D(GraphicsDevice, 256, 240);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Canvas);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(Canvas, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, CanvasScale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
