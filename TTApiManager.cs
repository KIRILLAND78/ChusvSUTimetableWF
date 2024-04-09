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
        public string State { get { return _state; } set { _state = value; StateChanged?.Invoke(value, strings); } }
        public delegate void StateHandler(string message, string[] strings);
        public event StateHandler StateChanged;
        private string _state = "Loading";
        public DateTime lastUpdate = DateTime.MinValue;
        public string[] strings;
        private string token { get { return $"{Settings.Instance.Session}:{TokenStore.Instance.Token}"; } }
        public TTApiManager()
        {
            strings = new string[9];
            if ((Settings.Instance.Key.Length == 0) ||
                (Settings.Instance.Session == 0) ||
                (new FileInfo("tk.dat").Length == 0)) State = "Not logged";
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
            Settings.Instance.Session = 0;
            Settings.Instance.Key = "";
            State = "Not logged";
        }
        public void UpdateData()
        {
            if (lastUpdate > DateTime.Now.AddMinutes(-30)) return;
            for (int i = 0; i < 9; i++) strings[i] = "-";
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://online.chuvsu.ru/api/v2/schedule/tomorrow")))
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", $"{Settings.Instance.Session}:{TokenStore.Instance.Token}");
                        var res = client.SendAsync(request).GetAwaiter().GetResult();
                        res.EnsureSuccessStatusCode();
                        var json = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var g = JsonDocument.Parse(json, new JsonDocumentOptions { MaxDepth = 50 });
                        var itemsVK = g.RootElement.GetProperty("items");
                        if (itemsVK.ToString()!="[]")
                        foreach (var itemVK in itemsVK.EnumerateObject())
                        {
                            foreach (var les in itemVK.Value.EnumerateArray())
                            {
                                var sb = les.GetProperty("subgroup").GetInt32();
                                    if ((Settings.Instance.Group!=0) && (!((sb == 0) || (sb == Settings.Instance.Group)))) continue;
                                //if (les.GetProperty("subgroup").GetInt32()<=1)
                                strings[les.GetProperty("pair").GetInt32() - 1] = $"{les.GetProperty("discipline")}({les.GetProperty("type").GetProperty("short")}) {les.GetProperty("cabinet").GetProperty("name").GetString()},  {les.GetProperty("start_time").GetString()} - {les.GetProperty("end_time").GetString()}";
                            }
                        }
                        State = "Nominal";
                        lastUpdate = DateTime.Now;
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
            UpdateData();
        }
    }
}
