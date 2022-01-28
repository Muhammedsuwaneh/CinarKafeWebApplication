using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CinarKafe.App_Start;

namespace CinarKafe
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // init automapper for dtos and os
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

            // configure api 
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
