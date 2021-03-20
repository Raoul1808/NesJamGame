using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Input;
using NesJamGame.Engine.IO;
using System.IO;
using System.Reflection;

namespace NesJamGame
{
    public class Program: Game
    {
        static void Main(string[] args)
        {
            Game game = new Program();

            var gamePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.CreateDirectory(Path.Combine(gamePath, "SaveData"));
            if (!File.Exists(SaveManager.FilePath))
            {
                SDL2.SDL.SDL_ShowSimpleMessageBox(SDL2.SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION, "Missing File", "Save File Missing! The game will automatically generate one.", game.Window.Handle);
                SaveManager.GenerateDefaultFile();
            }

            if (!File.Exists(ConfigManager.FilePath))
            {
                SDL2.SDL.SDL_ShowSimpleMessageBox(SDL2.SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION, "Missing File", "Configuration File Missing! The game will automatically generate one.", game.Window.Handle);
                ConfigManager.GenerateDefaultFile();
            }

            SaveManager.LoadFile();
            ConfigManager.LoadFile();

            game.Run();
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
            GlobalTime.Initialize();
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

        protected override void UnloadContent()
        {
            SaveManager.SaveJson();
            ConfigManager.SaveJson();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(CanvasScale);
            GlobalTime.Update(gameTime);
            // Uncommenting the line below would just crash the game, since there are no scenes yet in the game. -Mew
            // SceneManager.UpdateScenes();

            mouseInactiveTime += GlobalTime.ElapsedProgramMilliseconds / 1000;
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
            if (InputManager.GetMousePos() != null) TextRenderer.RenderText(spriteBatch, Text, new Point(0, 10));
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
