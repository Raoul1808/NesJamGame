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
            game = new Program();

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

        static bool stop = false;

        static Game game;
        public static int CanvasScale { get; private set; }
        public static int ScreenWidth { get { return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width; } }
        public static int ScreenHeight { get { return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height; } }
        public static double GameSpeed = 1;

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
            if (scale == null || Convert.ToInt32(scale) < 2 || !int.TryParse(scale, out _))
            {
                ConfigManager.SetValue("canvas_scale", "2");
                ConfigManager.SaveJson();
                scale = "2";
            }
            UpdateCanvasScale(Convert.ToInt32(scale));

            if (ConfigManager.GetValue("enable_sky") == null)
            {
                ConfigManager.SetValue("enable_sky", true.ToString());
                ConfigManager.SaveJson();
            }
            else if (!bool.TryParse(ConfigManager.GetValue("enable_sky"), out _))
            {
                ConfigManager.SetValue("enable_sky", true.ToString());
                ConfigManager.SaveJson();
            }

            if (SaveManager.GetValue("highscore") == null)
            {
                SaveManager.SetValue("highscore", "0");
                SaveManager.SaveJson();
            }
            else if (!int.TryParse(SaveManager.GetValue("highscore"), out _))
            {
                SaveManager.SetValue("highscore", "0");
                SaveManager.SaveJson();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentIndex.LoadContent(Content, GraphicsDevice);
            SceneManager.Initialize();
            SceneManager.AddScene("StartScene", new StartScene());
            SceneManager.AddScene("MenuScene", new MenuScene());
            SceneManager.AddScene("GameScene", new GameScene());

            TextRenderer.Initialize(ContentIndex.Textures["chars"], "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_!?><");

            Canvas = new RenderTarget2D(GraphicsDevice, 256, 240);
            sky = new SkyBackground();
        }

        protected override void Update(GameTime gameTime)
        {
            if (stop) Exit();
            InputManager.Update(CanvasScale);
            GlobalTime.Update(gameTime);
            GlobalTime.ChangeSpeed(GameSpeed);
            SceneManager.UpdateScenes();
            ParticleManager.UpdateParticles();
            if (Convert.ToBoolean(ConfigManager.GetValue("enable_sky"))) sky.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(Canvas);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            if (Convert.ToBoolean(ConfigManager.GetValue("enable_sky"))) sky.Draw(spriteBatch);
            SceneManager.DrawScenes(spriteBatch);
            ParticleManager.DrawParticles(spriteBatch);
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
            ConfigManager.SetValue("canvas_scale", CanvasScale.ToString());
        }

        public static void Quit()
        {
            stop = true;
        }

        public static void ShowSpeedAlert()
        {
            SDL.SDL_ShowSimpleMessageBox(SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING, "HOLD ON A SECOND!", "You are about to change the in-game speed. This feature can break the game in some ways. Use at your own risk!\nGame Speed resets back to default value (10) when starting the game.\nPlease do not report bugs that occur with a non-standard game speed!", game.Window.Handle);
        }
    }
}
