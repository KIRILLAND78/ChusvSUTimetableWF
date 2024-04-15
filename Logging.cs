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

            File.AppendAllText(path, $"\r\n{DateTime.Now}: {text}");
        }
        public static void Log(Exception ex)
        {

            File.AppendAllText(path, $"\r\n{DateTime.Now}: Exception Caught:\r\n");
            File.AppendAllText(path, ex.Message);
            File.AppendAllText(path, "\r\n");
            File.AppendAllText(path, ex.Source);
            File.AppendAllText(path, "\r\n");
            File.AppendAllText(path, ex.StackTrace);
        }
    }
}
