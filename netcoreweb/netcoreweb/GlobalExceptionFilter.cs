using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace netcoreweb
{
	/// <summary>
	/// 全局异常过滤器
	/// </summary>
	public class GlobalExceptionFilter : IExceptionFilter
	{
		readonly ILoggerFactory _loggerFactory;
		readonly IHostingEnvironment _env;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="loggerFactory"></param>
		/// <param name="env"></param>
		public GlobalExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
		{
			_loggerFactory = loggerFactory;
			_env = env;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public void OnException(ExceptionContext context)
		{
			var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

			logger.LogError(new EventId(context.Exception.HResult),
				context.Exception,
				context.Exception.Message);

			var json = new ErrorResponse("未知错误,请重试");

			if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;

			context.Result = new ApplicationErrorResult(json);
			context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

			//context.ExceptionHandled = true;
		}

		/// <summary>
		/// 
		/// </summary>
		public class ApplicationErrorResult : ObjectResult
		{
			/// <summary>
			/// 
			/// </summary>
			/// <param name="value"></param>
			public ApplicationErrorResult(object value) : base(value)
			{
				StatusCode = (int) HttpStatusCode.InternalServerError;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public class ErrorResponse
		{/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
			public ErrorResponse(string msg)
			{
				Message = msg;
			}
			/// <summary>
			/// 
			/// </summary>
			public string Message { get; set; }
			/// <summary>
			/// /
			/// </summary>
			public object DeveloperMessage { get; set; }
		}
	}
}
