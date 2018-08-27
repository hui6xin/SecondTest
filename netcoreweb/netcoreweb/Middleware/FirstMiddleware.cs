using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace netcoreweb.Middleware
{
    public class FirstMiddleware:IMiddleware
	{

		public Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			throw new NotImplementedException();
		}
	}
}
