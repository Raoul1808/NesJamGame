using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Input;

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
        int CanvasScale = 3;
        double mouseInactiveTime = 0;
        string Text = "HELLO WORLD!";

        public Program()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 256 * CanvasScale;
            graphics.PreferredBackBufferHeight = 240 * CanvasScale;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            InputManager.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SceneManager.Initialize();

            Texture2D chars = Content.Load<Texture2D>("chars");
            TextRenderer.Initialize(chars, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_!?><");

            Canvas = new RenderTarget2D(GraphicsDevice, 256, 240);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            // Uncommenting the line below would just crash the game, since there are no scenes yet in the game. -Mew
            // SceneManager.UpdateScenes();

            mouseInactiveTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (mouseInactiveTime >= 3)
            {
                IsMouseVisible = false;
            }
            if (InputManager.MouseStateChanged())
            {
                IsMouseVisible = true;
                mouseInactiveTime = 0;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Canvas);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // Uncommenting the line below would just crash the game, since there are no scenes yet in the game. -Mew
            // SceneManager.DrawScenes(spriteBatch);
            TextRenderer.RenderText(spriteBatch, Text, new Point(0, 10));
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(Canvas, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, CanvasScale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
