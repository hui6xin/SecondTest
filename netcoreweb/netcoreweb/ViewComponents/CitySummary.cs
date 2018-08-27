using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using netcoreweb.ViewModels;

namespace netcoreweb.ViewComponents
{
	public class CitySummary : ViewComponent
	{

		
		public CitySummary()
		{
			
		}

		public ViewViewComponentResult Invoke()
		{
			string target = RouteData.Values["id"] as string;

			//return $"4 cities, "
			//	   + $"4 people";
			return View(new CityViewModel(){ Cities="4",Population = "4"});
		}
	}
}
