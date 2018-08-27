using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcoreweb.BusinessLayer;

namespace netcoreweb.Controllers
{
    [Produces("application/json")]
    [Route("api/VerifyCode")]
    public class VerifyCodeController : Controller
    {
        // GET: api/VerifyCode
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

	    [HttpGet("GetImg/{guid}", Name = "GetGuid")]
	    public IActionResult Get(string guid)
	    {
			var bytes = VerifyCode.GetVerifyBytes(guid);
		    return File(bytes, "image/png");
		}

		[HttpGet("CreateNew/{*oldCodekey}", Name = "CreateNewVerifyCode")]
	    public string CreateNewVerifyCode(string oldCodeKey)
		{
			var code = VerifyCode.CreateNewVerifyCode(oldCodeKey);
			return code;
	    }

	    // POST: api/VerifyCode
		[HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/VerifyCode/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
