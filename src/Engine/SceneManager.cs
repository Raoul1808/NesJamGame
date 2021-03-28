using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace NesJamGame.Engine
{
    public static class SceneManager
    {
        static Dictionary<string, IScene> Scenes;
        static string CurrentScene;

        public static void Initialize()
        {
            Scenes = new Dictionary<string, IScene>();
            CurrentScene = null;
        }

        public static void AddScene(string name, IScene scene)
        {
            Scenes.Add(name, scene);
            if (CurrentScene == null)
            {
                CurrentScene = name;
            }
        }

        public static void RemoveScene(string name)
        {
            Scenes.Remove(name);
            if (CurrentScene == name)
            {
                CurrentScene = null;
            }
        }

        public static void ChangeScene(string name)
        {
            CurrentScene = name;
        }

        public static void RefreshScene(string name)
        {
            Scenes[name] = (IScene)Activator.CreateInstance(Scenes[name].GetType());
        }

        public static void UpdateScenes()
        {
            Scenes[CurrentScene].Update();
        }

        public static void DrawScenes(SpriteBatch spriteBatch)
        {
            Scenes[CurrentScene].Draw(spriteBatch);
        }
    }
}
