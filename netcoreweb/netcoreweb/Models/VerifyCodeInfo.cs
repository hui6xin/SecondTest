using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcoreweb.Models
{
	public struct VerifyCodeInfo
	{
		public string Guid;
		public string Code;
		public DateTime CretetimeDateTime;
		public VerifyCodeInfo(string guid, string code) : this(guid, code, DateTime.Now)
		{
		}

		public VerifyCodeInfo(string guid, string code, DateTime createDateTime)
		{
			Guid = guid;
			Code = code;
			CretetimeDateTime = createDateTime;
		}


	}
}
