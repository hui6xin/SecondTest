using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using netcoreweb.ViewModels;

namespace netcoreweb.Filters
{
	public class HeaderFooterFilter : ActionFilterAttribute
	{
		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var v = filterContext.Result as ViewResult;
			if (v != null) // v will null when v is not a ViewResult
			{
				var bvm = v.Model as BaseViewModel;
				if (bvm != null) //bvm will be null when we want a view without Header and footer
				{
					bvm.UserName = filterContext.HttpContext.User.Identity.Name;
					bvm.FooterData = new FooterViewModel();
					bvm.FooterData.CompanyName = "StepByStepSchools"; //Can be set to dynamic value
					bvm.FooterData.Year = DateTime.Now.Year.ToString();
				}
			}
		}
	}
}