using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace netcoreweb.Filters
{
	/// <summary>
	/// </summary>
	public class AdminFilter : ActionFilterAttribute
	{
		/// <summary>
		/// </summary>
		/// <param name="filterContext"></param>
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!Convert.ToBoolean(filterContext.HttpContext.Session.GetString("IsAdmin")))
				filterContext.Result = new ContentResult
				{
					Content = "Unauthorized to access specified resource."
				};
		}
	}
}