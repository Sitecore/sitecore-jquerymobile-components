using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines;
using System.Web.Routing;
using Sitecore.Mvc.Configuration;
using System.Web.Mvc;
using Sitecore.Mvc.Presentation;
using Sitecore.Mvc.Extensions;

namespace SitecoreMobile.Pipelines.Loaders
{
    public class InitRoutes 
    {
        public virtual void Process(PipelineArgs args)
        {
            this.RegisterRoutes(RouteTable.Routes, args);
        }

        protected virtual void RegisterRoutes(System.Web.Routing.RouteCollection routes, PipelineArgs args)
        {
            MapSearchController(routes);
        }

        private static void MapSearchController(System.Web.Routing.RouteCollection routes)
        {
            System.Web.Mvc.RouteCollectionExtensions.MapRoute(routes, "SearchRoute", "search/{action}/{*scItemPath}",
                new
                {
                    controller = "Search",
                    scKeysToIgnore = new string[] { }
                },
                new
                {
                }
            );
        }

        public class isValidId : IRouteConstraint
        {
            public bool Match(
                HttpContextBase httpContext,
                Route route,
                string parameterName,
                RouteValueDictionary values,
                RouteDirection routeDirection)
            {
                string str = values["id"] as string;
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
                else
                {
                    // run better check for id existance
                    return true;
                }
            }
        }



        public class IsContentUrlRestraint : IRouteConstraint
        {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                if (routeDirection == RouteDirection.IncomingRequest)
                {
                    string str = httpContext.Items[(object)"sc::IsContentUrl"] as string;
                    if (str == null)
                        return false;
                    else
                        return StringExtensions.ToBool(str);
                }
                else
                {
                    return true;
                }
            }
        }

    }
}