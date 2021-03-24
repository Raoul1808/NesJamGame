using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NesJamGame.Engine;
using NesJamGame.Engine.Graphics;
using NesJamGame.Engine.Input;
using NesJamGame.Engine.IO;
using NesJamGame.GameContent;
using NesJamGame.GameContent.Scenes;
using SDL2;
using System;
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

            SaveManager.SaveJson();
            ConfigManager.SaveJson();
        }

        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RenderTarget2D Canvas;
        SkyBackground sky;

        public static int CanvasScale { get; private set; }

        public Program()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            UpdateCanvasScale(3);
        }

        protected override void Initialize()
        {
            InputManager.Initialize();
            GlobalTime.Initialize();

            string scale = ConfigManager.GetValue("canvas_scale");
            if (scale == null)
            {
                ConfigManager.SetValue("canvas_scale", "2");
                ConfigManager.SaveJson();
                scale = "2";
            }
            UpdateCanvasScale(Convert.ToInt32(scale));

            if (ConfigManager.GetValue("window_shake") == null)
            {
                ConfigManager.SetValue("window_shake", "true");
                ConfigManager.SaveJson();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentIndex.LoadContent(Content, GraphicsDevice);
            SceneManager.Initialize();
            SceneManager.AddScene("GameScene", new GameScene());

            TextRenderer.Initialize(ContentIndex.Textures["chars"], "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_!?><");

            Canvas = new RenderTarget2D(GraphicsDevice, 256, 240);
            sky = new SkyBackground();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(CanvasScale);
            GlobalTime.Update(gameTime);
            SceneManager.UpdateScenes();
            sky.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Canvas);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            sky.Draw(spriteBatch);
            SceneManager.DrawScenes(spriteBatch);
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            spriteBatch.Draw(Canvas, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, CanvasScale, SpriteEffects.None, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void UpdateCanvasScale(int newScale)
        {
            CanvasScale = newScale;
            graphics.PreferredBackBufferWidth = 256 * CanvasScale;
            graphics.PreferredBackBufferHeight = 240 * CanvasScale;
            graphics.ApplyChanges();
        }
    }
}
