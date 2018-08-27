using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace netcoreweb.Models
{
	public class ZookeeperNodeValue
	{
		public List<ZookeeperNodeValue> ChildList;
		public string NodeName;
		public string Path;
		public string Data;
		private const string RegexStr = @"(\/[a-z0-9_]+)";
		private Regex rg = new Regex(RegexStr, RegexOptions.None);
		private bool isRoot = false;

		public bool IsRoot
		{
			get
			{
				if (string.IsNullOrEmpty(Path) || rg.Matches(Path).Count > 1)
					return false;
				return isRoot;
			}
		}

		public ZookeeperNodeValue()
		{
		}

		public ZookeeperNodeValue(string path)
		{
			SetPath(path);
		}

		public void SetPath(string path)
		{
			if (rg.IsMatch(path))
			{
				Path = path;
				var index = Path.LastIndexOf(@"/", StringComparison.Ordinal) + 1;
				NodeName = Path.Substring(index, Path.Length - index);
			}
		}

		public ZookeeperNodeValue GetChildNode(string nodeName)
		{
			var nodetemp = ChildList.First(x => x.NodeName == nodeName);
			return nodetemp;
		}

		public void SetData(byte[] bytes)
		{
			Data = System.Text.Encoding.Default.GetString(bytes);
		}

		public void SetChildNode(IEnumerable<string> nodeList)
		{
			ChildList.GetEnumerator().Dispose();
			foreach (var node in nodeList)
			{
				var nodetemp = new ZookeeperNodeValue(Path + @"/" + node);
				ChildList.Add(nodetemp);
			}
		}
	}
}
