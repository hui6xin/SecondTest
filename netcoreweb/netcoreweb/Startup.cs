using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

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
		    services.AddMvc();
		    services.AddSwaggerGen(options =>
		    {
			    options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
			    {
				    Version = "v1",
				    Title = " API 文档",
				    Description = "sdfsdfsdf"
			    });

			    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
			    var xmlPath = Path.Combine(basePath, "netcoreweb.xml");
			    options.IncludeXmlComments(xmlPath);
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
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

	        app.UseSwagger();
	        app.UseSwaggerUI(c =>
	        {
		        c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
	        });
		}
    }
}
