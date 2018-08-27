using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace netcoreweb.Controllers
{/// <summary>
/// 
/// </summary>
	[Produces("application/json")]
	[ApiVersion("1.0")]
	[Route("api/Value")]
	public class ValueController : Controller
	{
		/// <summary>
		///     get
		/// </summary>
		/// <returns></returns>
		[MapToApiVersion("1.0")]
		[HttpGet]	
		public IEnumerable<string> Get()
		{
			return new[] {"value1", "value2"};
		}
		[MapToApiVersion("3.0")]
		[HttpGet(Name = "GetV3")]
		public IEnumerable<string> GetV3()
		{
			return new[] { "value3"};
		}

		/// <summary>
		///     getbyid
		/// </summary>
		/// <param name="id">id</param>
		/// <returns>返回值</returns>
		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}