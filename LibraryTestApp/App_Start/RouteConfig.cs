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
                url: "{firstName}_{lastName}",
                defaults: new { controller = "Author", action = "EditAuthor" },
                constraints: new { firstName = new AuthorFirstNameConstraint(), lastName = new AuthorLastNameConstraint()}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

public class AuthorFirstNameConstraint : IRouteConstraint
{
    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    {
        using (var ctx = new BooksCatalogue())
        {
            List<string> authorsFirstNames = ctx.Authors.Select(a => a.FirstName).ToList();
       
        // Get the username from the url
        var authorFirstName = values["firstName"].ToString().Replace("_", " ").ToLower();
        // Check for a match (assumes case insensitive)
            return authorsFirstNames.Any(x => x.ToLower() == authorFirstName.Replace("_"," "));
        }
    }
}
public class AuthorLastNameConstraint : IRouteConstraint
{
    public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
    {
        using (var ctx = new BooksCatalogue())
        {
            List<string> authorsLastNames = ctx.Authors.Select(a => a.LastName).ToList();

            // Get the username from the url
            var authorLastName = values["lastName"].ToString().Replace("_", " ").ToLower();
            // Check for a match (assumes case insensitive)
                return authorsLastNames.Any(x => x.ToLower() == authorLastName.Replace("_", " "));
        }
    }
}
