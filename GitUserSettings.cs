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
		public List<GitRepository> Repositories { get; set; }
		public string FocusedRepository { get; set; }
        public string LastBrowsedFolder { get; set; }
		public string ExternalDiffApplication { get; set; }
		public string ExternalDiffArgumentsFormat { get; set; }

        public GitUserSettings()
		{
			Repositories = new List<GitRepository>();
			ExternalDiffArgumentsFormat = "{0} {1}";
		}
	}

	public class GitRepository
	{
		public string RemoteURL { get; set; }
		public string LocalPath { get; set; }
	}
}
