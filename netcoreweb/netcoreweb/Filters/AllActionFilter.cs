using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace netcoreweb.Filters
{
	/// <summary>
	/// 所有的action记录
	/// </summary>
    public class AllActionFilter : IActionFilter
    {
		/// <summary>
		/// 计时器
		/// </summary>
	    Stopwatch sw = new Stopwatch();
		/// <summary>
		/// action执行触发事件
		/// </summary>
		/// <param name="context"></param>
		public void OnActionExecuting(ActionExecutingContext context)
	    {
		    var c = context.Controller;
		    var actiondiscrib = context.ActionDescriptor;
		
		    var type = c.GetType();
		    var hashcode = c.GetHashCode();
		    var version = context.HttpContext.GetRequestedApiVersion().ToString();
		    var list = CustomAttributeData.GetCustomAttributes(type);

		    var ss = new { type, hashcode, version, list };
			
			Debug.WriteLine("OnActionExecuting");

		    //Debug.WriteLine(JsonConvert.SerializeObject(ss));
		    sw.Start();
		}

	    public void OnActionExecuted(ActionExecutedContext context)
	    {
		    var c = context.Controller;
		    var type = c.GetType();
		    var hashcode = c.GetHashCode();

		    Debug.WriteLine("OnActionExecuted");

		    Debug.WriteLine(type.ToString() + " " + hashcode);
		    sw.Stop();
		    Debug.WriteLine(sw.ElapsedTicks / (decimal)Stopwatch.Frequency);
		}
    }
}
