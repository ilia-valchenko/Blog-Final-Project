using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Hashtag",
                url: "Hashtag/{tagname}",
                defaults: new { controller = "Post", action = "SearchByTag" }
            );

            routes.MapRoute(
                name: "ChangeAvatar",
                url: "User/ChangeAvatar",
                defaults: new { controller = "User", action = "ChangeAvatar" }
            );

            routes.MapRoute(
                name: "UserProfile",
                url: "User/{nickname}",
                defaults: new { controller = "User", action = "UserProfile" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "404-PageNotFound",
                url: "{*url}",
                defaults: new { controller = "Error", action = "NotFound" }
            );
        }
    }
}
