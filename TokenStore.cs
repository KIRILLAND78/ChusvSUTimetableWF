using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace ChusvSUTimetableWF
{
    internal class TokenStore
    {
        public static TokenStore Instance {  get { if (_instance == null) _instance = new(); return _instance; } }
        static TokenStore? _instance;
        public static string Path => $"{Settings.Folder}/tk.dat";
        public TokenStore()
        {
            if (!Directory.Exists(Settings.Folder))
                Directory.CreateDirectory(Settings.Folder);
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, JsonSerializer.Serialize(this));
            }
        }
        public string Token {
            get
            {
                if (Settings.Instance.Key.Length == 0) return "";
                FileStream fStream = new FileStream(Path, FileMode.OpenOrCreate);

                long len = new FileInfo(Path).Length;
                byte[] inBuffer = new byte[len];
                fStream.Read(inBuffer, 0, (int)len);
                var key = Encoding.Unicode.GetBytes(Settings.Instance.Key);
                key[0] = 78;
                byte[] decryptData = ProtectedData.Unprotect(inBuffer, key, DataProtectionScope.CurrentUser);
                fStream.Dispose();
                return Encoding.ASCII.GetString(decryptData);
            }
            set
            {
                FileStream fStream = new FileStream(Path, FileMode.OpenOrCreate);
                byte[] toEncrypt = Encoding.ASCII.GetBytes(value);
                if (toEncrypt.Length <= 0)
                    throw new ArgumentException("The buffer length was 0.", nameof(Buffer));
                byte[] key = RandomNumberGenerator.GetBytes(16);
                Settings.Instance.Key = Encoding.Unicode.GetString(key);
                Settings.Instance.Save();
                key[0] = 78;
                byte[] encryptedData = ProtectedData.Protect(toEncrypt, key, DataProtectionScope.CurrentUser);
                fStream.Write(encryptedData, 0, encryptedData.Length);
                fStream.Dispose();
            }
        
        }
        private int token;
    }
}
