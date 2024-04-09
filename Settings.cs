using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChusvSUTimetableWF
{
    internal class Settings
    {
        public static Settings Instance {  get { if (_instance == null) _instance = new(); return _instance; } }
        static Settings? _instance;
        private const string path= "st.json";
        public Settings()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, JsonSerializer.Serialize(this));
            }
            using (var jsonDocument = JsonDocument.Parse(File.ReadAllText(path)))
            {
                try
                {
                    x = jsonDocument.RootElement.GetProperty("X").GetInt32();
                    y = jsonDocument.RootElement.GetProperty("Y").GetInt32();
                    key = jsonDocument.RootElement.GetProperty("Key").GetString() ?? "";
                    session = jsonDocument.RootElement.GetProperty("Session").GetInt32();
                    group = jsonDocument.RootElement.GetProperty("Group").GetInt32();
                } catch (KeyNotFoundException ex) { //это нормально.
                }
            }

        }
        public int X { get { return x; } set { x = value; } }
        private int x;
        public int Y { get { return y; } set { y = value; } }
        private int y;
        public string Key { get { return key; } set { key = value; } }
        private string key = "";
        public int Session { get { return session; } set { session = value; } }
        private int session;
        public int Group { get { return group; } set { group = value; } }
        private int group;
        public void Save()
        {
            File.WriteAllText(path, JsonSerializer.Serialize(this));
        }
    }
}
