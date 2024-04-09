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
        const string path = "log.txt";
        public static void Log(string text)
        {

            if (!File.Exists(path))
            {
                File.AppendAllText(path, $"{DateTime.Now}: {text}");
            }
        }
        public static void Log(Exception ex)
        {

            if (!File.Exists(path))
            {
                File.AppendAllText(path, $"{DateTime.Now}: Exception Caught:");
                File.AppendAllText(path, ex.Message);
                File.AppendAllText(path, ex.Source);
                File.AppendAllText(path, ex.StackTrace);
            }
        }
    }
}
