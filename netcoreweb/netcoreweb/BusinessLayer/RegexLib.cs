using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace netcoreweb.BusinessLayer
{
	public class RegexLib
	{
		public static bool IsPhoneNum(string phoneInput)
		{
			var regex = new Regex(@"^1(3|4|5|7|8|9)\d{9}$", RegexOptions.IgnorePatternWhitespace);
			return regex.IsMatch(phoneInput);
		}

		public static bool IsValidPassWord(string passWord)
		{
			var regex = new Regex(@"^(?!(?:\d+|[a-zA-Z]+)$)[\da-zA-Z]{6,16}$", RegexOptions.IgnorePatternWhitespace);
			return regex.IsMatch(passWord);
		}

		public static bool IsValidUserCode(string code)
		{
			var regex = new Regex(@"^[A-Za-z]+[a-zA-Z0-9_]*$", RegexOptions.IgnorePatternWhitespace);
			return regex.IsMatch(code);
		}
		public static bool IsValidCode(string code)
		{

			var regex = new Regex(@"^[a-zA-Z\d]{6,12}$", RegexOptions.IgnorePatternWhitespace);
			return regex.IsMatch(code);
		}

		public static bool IsAllNum(string str)
		{
			var regex = new Regex(@"[0-9]+", RegexOptions.IgnorePatternWhitespace);
			return regex.IsMatch(str);
		}
		public static bool UserName(string name)
		{
			var regex = new Regex(@"(^[\u4e00-\u9fa5]{1,7}$)|^([A-Za-z]{1,14}$)", RegexOptions.IgnorePatternWhitespace);
			return regex.IsMatch(name);
		}
	}
}
