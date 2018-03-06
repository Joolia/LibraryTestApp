using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LibraryTestApp.EntityClasses;

namespace LibraryTestApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Author",
                url: "Author/{fullname}",
                defaults: new { controller = "Author", action = "EditAuthor", fullname = UrlParameter.Optional },
                constraints: new {fullname = new AuthorLastNameConstraint()}
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

public class AuthorLastNameConstraint : IRouteConstraint
{
    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    {
        using (var ctx = new BooksCatalogue())
        {
            List<string> authorsFirstNames = ctx.Authors.Select(a => a.FirstName).ToList();
            List<string> authorsLastNames = ctx.Authors.Select(a => a.LastName).ToList();

            // Get the username from the url
            var authorName = values["fullname"].ToString().Split('_');

            return authorName.Count() == 2;
        }
    }
}
