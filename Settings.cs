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
        public static string Folder => $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/TimetableWidget/";
        private static string Path => $"{Folder}/st.json";
        public Settings()
        {
            if (!Directory.Exists(Folder))
                Directory.CreateDirectory(Folder);
            if (!File.Exists(Path))
            {
                Logging.Log("Settings file does not exist, creating one.");
                File.WriteAllText(Path, JsonSerializer.Serialize(this));
            }
            using (var jsonDocument = JsonDocument.Parse(File.ReadAllText(Path)))
            {
                try
                {
                    x = jsonDocument.RootElement.GetProperty("X").GetInt32();
                    y = jsonDocument.RootElement.GetProperty("Y").GetInt32();
                    key = jsonDocument.RootElement.GetProperty("Key").GetString() ?? "";
                    session = jsonDocument.RootElement.GetProperty("Session").GetInt32();
                    group = jsonDocument.RootElement.GetProperty("Group").GetInt32();
                    transparency = jsonDocument.RootElement.GetProperty("Transparency").GetInt32();
                    draggable = jsonDocument.RootElement.GetProperty("Draggable").GetBoolean();
                    debugMode = jsonDocument.RootElement.GetProperty("DebugMode").GetBoolean();
                } catch (KeyNotFoundException ex) { //это нормально.
                    Logging.Log("JSON seems to be malformed:");
                    Logging.Log(ex);
                } catch(Exception ex) { Logging.Log(ex);}
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
        public int Transparency { get { return transparency; } set { transparency = value; } }
        private int transparency = 100;
        public bool Draggable { get { return draggable; } set { draggable = value; } }
        private bool draggable = true;
        public bool DebugMode { get { return debugMode; } set { debugMode = value; } }
        private bool debugMode = false;
        public void Save()
        {
            File.WriteAllText(Path, JsonSerializer.Serialize(this));
            TTApiManager.Instance.lastUpdate = DateTime.Now.AddMinutes(-1);
            TTApiManager.Instance.UpdateData();
            Program.main.MakeWin();
        }
    }
}
