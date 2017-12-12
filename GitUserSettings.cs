using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PaJaMa.GitStudio
{
	public class GitUserSettings
	{
		[XmlIgnore]
		public const string PASSWORD = "GIT_CREDENTS";

		public List<GitAccount> Accounts { get; set; }
		public List<GitRepository> Repositories { get; set; }
		public string FocusedRepository { get; set; }
        public string LastBrowsedFolder { get; set; }

        public GitUserSettings()
		{
			Accounts = new List<GitAccount>();
			Repositories = new List<GitRepository>();
		}
	}

	public class GitAccount
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		[XmlIgnore]
		public string PasswordDecrypted
		{
			get { return EncrypterDecrypter.Instance.Decrypt(Password, GitUserSettings.PASSWORD); }
			set { Password = EncrypterDecrypter.Instance.Encrypt(value, GitUserSettings.PASSWORD); }
		}

		public override string ToString()
		{
			return UserName;
		}
	}

	public class GitRepository
	{
		[XmlIgnore]
		public string RemoteURLDecrypted
		{
			get { return EncrypterDecrypter.Instance.Decrypt(RemoteURL, GitUserSettings.PASSWORD); }
			set { RemoteURL = EncrypterDecrypter.Instance.Encrypt(value, GitUserSettings.PASSWORD); }
		}

		public string RemoteURL { get; set; }
		public string LocalPath { get; set; }
		public string UserName { get; set; }
	}
}
