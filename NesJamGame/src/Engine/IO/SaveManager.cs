using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NesJamGame.Engine.IO
{
    public static class SaveManager
    {
        public static string FilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SaveData", "Save.json");
        static Dictionary<string, string> save;
        
        public static void LoadFile()
        {
            save = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(FilePath));
        }

        public static void GenerateDefaultFile()
        {
            File.CreateText(FilePath).Close();
            save = new Dictionary<string, string>();
            SaveJson();
        }

        public static string GetValue(string key)
        {
            if (save.ContainsKey(key)) return save[key];
            else return null;
        }

        public static void SetValue(string key, string value)
        {
            if (save.ContainsKey(key))
            {
                save[key] = value;
            }
            else
            {
                save.Add(key, value);
            }
        }

        public static void SaveJson()
        {
            var stream = new StreamWriter(FilePath);
            string json = JsonConvert.SerializeObject(save, Formatting.Indented);
            stream.Write(json);
            stream.Close();
        }
    }
}
