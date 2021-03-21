using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NesJamGame.Engine.IO
{
    public class ConfigManager
    {
        public static string FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SaveData", "Config.json");
        static Dictionary<string, string> config;

        public static void LoadFile()
        {
            config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(FilePath));
        }

        public static void GenerateDefaultFile()
        {
            File.CreateText(FilePath).Close();
            config = new Dictionary<string, string>();
            SaveJson();
        }

        public static string GetValue(string key)
        {
            if (config.ContainsKey(key)) return config[key];
            else return null;
        }

        public static void SetValue(string key, string value)
        {
            if (config.ContainsKey(key))
            {
                config[key] = value;
            }
            else
            {
                config.Add(key, value);
            }
        }

        public static void SaveJson()
        {
            var stream = new StreamWriter(FilePath);
            string json = JsonConvert.SerializeObject(config, Formatting.Indented);
            stream.Write(json);
            stream.Close();
        }
    }
}
