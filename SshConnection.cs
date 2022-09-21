using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.GitStudio
{
    public class SshConnection
    {
        private const string PASSWORD = "SS4CONN";

        public string Host { get; set; }
        public string UserName { get; set; }
        public string Path { get; set; }
        public string PasswordEncrypted { get; set; }
        public string KeyFile { get; set; }
        public bool UseCMD { get; set; }

        [JsonIgnore]
        public string Password
        {
            get { return string.IsNullOrEmpty(PasswordEncrypted) ? string.Empty : Common.EncrypterDecrypter.Decrypt(PasswordEncrypted, PASSWORD); }
            set { PasswordEncrypted = string.IsNullOrEmpty(value) ? value : Common.EncrypterDecrypter.Encrypt(value, PASSWORD); }
        }
    }
}
