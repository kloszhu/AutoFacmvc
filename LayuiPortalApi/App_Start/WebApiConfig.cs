using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LayuiPortalApi
{
    public static class WebApiConfig
    {
        private const string V = "*";

        public static void Register(HttpConfiguration config)
        {
            //跨域配置.
            config.EnableCors(new EnableCorsAttribute(V, V,V));
            // Web API 配置和服务

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
