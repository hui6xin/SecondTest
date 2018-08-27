using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace netcoreweb.ViewComponents
{
	public class CityWeather : ViewComponent
	{
		private static readonly Regex WeatherRegex = new Regex(@"<img id=cur-weather class=mtt title="".+?"" src=""//(.+?.png)"" width=80 height=80>");

		public async Task<IViewComponentResult> InvokeAsync()
		{
			string country = "china";
			string city = "nanjing";// city.Replace(" ", string.Empty);
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync($"https://www.timeanddate.com/weather/{country}/{city}");
				var content = await response.Content.ReadAsStringAsync();
				var match = WeatherRegex.Match(content);
				var imageUrl = match.Groups[1].Value;
				return View("Default", imageUrl);
			}
		}
	}
}
