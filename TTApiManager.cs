using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ChusvSUTimetableWF
{
    internal class TTApiManager
    {
        public static TTApiManager Instance { get { if (_instance == null) _instance = new(); return _instance; } }
        static TTApiManager? _instance;
        public string State { get { return _state; } set {
                _state = value;
                StateChanged?.Invoke(value, strings, additionalData);
            } }
        bool isToday = true;
        public string DayName { get { if (lastUpdate == DateTime.MinValue) return "Загрузка..."; if (isToday) return $"сегодня, {WeekDayName}"; return $"завтра, {WeekDayName}"; } }
        public string WeekDayName { get { if (lastUpdate == DateTime.MinValue) return "...";
                var forDate = DateTime.Now;
                if (!isToday) forDate = forDate.AddDays(1);
                return (new string[7]{"Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"})[(int)forDate.DayOfWeek];
            } }
        public delegate void StateHandler(string message, string[] strings, string[] additionalData);
        public event StateHandler StateChanged;
        private string _state = "Loading";
        public DateTime lastUpdate = DateTime.MinValue;
        public string[] strings;
        public string[] additionalData;
        private static string Path => $"{Settings.Folder}/result.json";
        private string token { get { return $"{Settings.Instance.Session}:{TokenStore.Instance.Token}"; } }
        public TTApiManager()
        {
            strings = new string[9];
            additionalData = new string[9];
            if ((Settings.Instance.Key.Length == 0) ||
                (Settings.Instance.Session == 0) || (!File.Exists(TokenStore.Path)) ||
                (new FileInfo(TokenStore.Path).Length == 0)) State = "Not logged";
            else State = "Logged";
        }
        public void LoginCallInput()
        {
            if (State == "Not logged")
            {
                Form2 f = new Form2();
                f.Show();
            }
            else
            {
                UpdateData();
            }
        }
        public void Logout()
        {
            strings = new string[9];
            additionalData = new string[9];
            for (int i = 0; i < 9; i++) strings[i] = "-";
            for (int i = 0; i < 9; i++) additionalData[i] = "-";
            Settings.Instance.Session = 0;
            Settings.Instance.Key = "";
            Settings.Instance.Save();
            State = "Not logged";
        }
        public void UpdateData()
        {
            //???
            var forcedUpdate = false;
            var las = lastUpdate.AddDays(1).ToUniversalTime().AddHours(3).AddMinutes(-10).Date;
            var tom = DateTime.UtcNow.AddHours(3).Date;
            if ( las < tom )
            {
                forcedUpdate = true;
                isToday = true;
            }
            //read from json, take last time force forcedUpdate to true if needed

            if (!(forcedUpdate))
                if (lastUpdate > DateTime.Now.AddMinutes(-30)) { State = State; return; }
            for (int i = 0; i < 9; i++) strings[i] = "-";
            for (int i = 0; i < 9; i++) additionalData[i] = "-";
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://online.chuvsu.ru/api/v2/schedule/"+(isToday?"today":"tomorrow"))))
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
                        var res = client.SendAsync(request).GetAwaiter().GetResult();
                        res.EnsureSuccessStatusCode();
                        var json = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        File.WriteAllText(Path, json);
                        var g = JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 50 });
                        var itemsVK = g.RootElement.GetProperty("items");
                        var switchToTomorrow = isToday;
                        if (itemsVK.ToString()!="[]")
                        foreach (var itemVK in itemsVK.EnumerateObject())
                        {
                            foreach (var les in itemVK.Value.EnumerateArray())
                            {
                                var sb = les.GetProperty("subgroup").GetInt32();
                                    if ((Settings.Instance.Group!=0) && (!((sb == 0) || (sb == Settings.Instance.Group)))) continue;
                                    //if (les.GetProperty("subgroup").GetInt32()<=1)
                                    if (DateTime.Parse(les.GetProperty("end_time").GetString())>DateTime.UtcNow.AddHours(3).AddMinutes(10))
                                    {
                                        switchToTomorrow = false;
                                    }
                                    strings[les.GetProperty("pair").GetInt32() - 1] = $"{les.GetProperty("discipline")}";
                                    additionalData[les.GetProperty("pair").GetInt32() - 1] = $"{les.GetProperty("start_time").GetString()} - {les.GetProperty("end_time").GetString()}   {les.GetProperty("cabinet").GetProperty("name").GetString()}   {les.GetProperty("type").GetProperty("short")}";
                            }
                        }
                        if (switchToTomorrow)
                        {
                            var was = isToday;
                            if (isToday)
                            {
                                isToday = false;
                            }
                            if (was!=isToday)
                                UpdateData();
                        }
                        lastUpdate = DateTime.Now;
                        State = "Nominal";
                    }
                    catch (Exception ex)
                    {

                        //Log(ex.Message);
                        //Log(ex.InnerException.Message);
                        //Log(ex.HelpLink);
                        State = $"Error";
                        Logging.Log(ex);
                    }
                    //res.Dispose();
                    //g.Dispose();
                }
            }
        }
        public void Login(string mail, string pass)
        {
            Logout();
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://online.chuvsu.ru/api/v2/token")))
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Base", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{mail}:{pass}")));
                        var res = client.SendAsync(request).GetAwaiter().GetResult();
                        //res.EnsureSuccessStatusCode();
                        var json = res.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
                        var g = JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 50 });
                        try
                        {
                            var isAuthenticated = g.RootElement.GetProperty("status").GetString();
                            if ((isAuthenticated == "unauthorized") || (isAuthenticated == "error"))
                            {
                                State = "Wrong credentials";
                                return;
                            }
                        } catch (Exception) { }//все нормально
                        var tokenVK = g.RootElement.GetProperty("message").GetProperty("token");
                        var sessionIdVK = g.RootElement.GetProperty("message").GetProperty("id");
                        TokenStore.Instance.Token = tokenVK.GetString();
                        Settings.Instance.Session = sessionIdVK.GetInt32();
                        State = "Nominal";
                        Settings.Instance.Save();
                    }
                    catch (Exception ex)
                    {
                        State = $"Error";
                        Logging.Log(ex);
                        Logout();
                    }
                }
            }
            lastUpdate = DateTime.MinValue;
            UpdateData();
        }
    }
}
