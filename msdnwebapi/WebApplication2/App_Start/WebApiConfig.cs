﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApplication2.Formatters;

namespace WebApplication2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            config.Formatters.Add(new ProductCsvFormatter());
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
           
        }
    }
}
