using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using netcoreweb.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace netcoreweb
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddMvc(options =>
			{
				//options.Filters.Add<GlobalExceptionFilter>();
				options.Filters.Add<AllActionFilter>();
			});
			services.AddApiVersioning(options =>
			{
				options.ReportApiVersions = true;
				options.ApiVersionReader = ApiVersionReader.Combine
				(
					new QueryStringApiVersionReader("api-version"),
					new HeaderApiVersionReader("api-version")
				);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1,0);
			});
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Info
				{
					Version = "v1",
					Title = " API 文档",
					Description = "sdfsdfsdf"
				});
				var basePath = PlatformServices.Default.Application.ApplicationBasePath;
				var xmlPath = Path.Combine(basePath, "netcoreweb.xml");
				options.IncludeXmlComments(xmlPath);
				options.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());
			});
			services.AddSession();
			//services.AddAuthentication();
			services.AddAuthentication(b =>
			{
				b.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				b.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				b.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, b =>
			{
				//登陆地址
				b.LoginPath = "/Authentication/index";
				//sid
				b.Cookie.Name = "Cookie_R";
				//b.Cookie.Domain = "localhost";
				//b.Cookie.Path = "/";
				//b.Cookie.HttpOnly = true;
				b.Cookie.Expiration = new TimeSpan(1, 30, 30);
				b.ExpireTimeSpan = new TimeSpan(1, 30, 30);
			});
			services.Configure<FormOptions>(x =>
			{
				x.ValueLengthLimit = int.MaxValue;
				x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseStatusCodePages();
			}

			app.UseStaticFiles();
			app.UseSession();
			//验证中间件
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"default",
					"{controller=Employee}/{action=Index}/{id?}");
			});


			//app.UseStaticFiles();

			//app.UseMvcWithDefaultRoute();

			app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs"); });
		}
	}
}