using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Input;
using NesJamGame.Engine.IO;
using NesJamGame.GameContent;
using SDL2;
using System.IO;
using System.Reflection;

namespace NesJamGame
{
    public class Program : Game
    {
        static void Main(string[] args)
        {
            Game game = new Program();

            var gamePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.CreateDirectory(Path.Combine(gamePath, "SaveData"));
            if (!File.Exists(SaveManager.FilePath))
            {
                SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION, "Missing File", "Save File Missing! The game will automatically generate one.", game.Window.Handle);
                SaveManager.GenerateDefaultFile();
            }

            if (!File.Exists(ConfigManager.FilePath))
            {
                SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION, "Missing File", "Configuration File Missing! The game will automatically generate one.", game.Window.Handle);
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
            SceneManager.AddScene("DevScene", new DevScene(Content));

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
            SceneManager.UpdateScenes();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Canvas);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            SceneManager.DrawScenes(spriteBatch);
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
