using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChusvSUTimetableWF
{
    internal static class Logging
    {
        private static string Path => $"{Settings.Folder}/log.txt";
        static Logging()
        {
            Directory.CreateDirectory(Settings.Folder);
        }
        public static void Log(string text)
        {

            File.AppendAllText(Path, $"\r\n{DateTime.Now}: {text}");
        }
        public static void Log(Exception ex)
        {

            File.AppendAllText(Path, $"\r\n{DateTime.Now}: Exception Caught:\r\n");
            File.AppendAllText(Path, ex.Message);
            File.AppendAllText(Path, "\r\n");
            File.AppendAllText(Path, ex.Source);
            File.AppendAllText(Path, "\r\n");
            File.AppendAllText(Path, ex.StackTrace);
        }
    }
}
