using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.GitStudio
{
	public abstract class Branch
	{
		public string BranchID { get; set; }
		public string BranchName { get; set; }

		public override string ToString()
		{
			return BranchName;
		}
	}

	public class LocalBranch : Branch
	{
		public RemoteBranch TracksBranch { get; set; }
		public bool RemoteIsGone { get; set; }
		public int Behind { get; set; }
		public int Ahead { get; set; }
		public bool IsCurrent { get; set; }
	}

	public class RemoteBranch : Branch
	{

	}

	public class Difference
	{
		public DifferenceType DifferenceType { get; set; }
		public string FileName { get; set; }
		public bool IsStaged { get; set; }
		public bool IsConflict { get; set; }
	}

	public enum DifferenceType
	{
		Add,
		Modify,
		Delete,
		Rename
	}
}
