namespace EmployeeAPP.Helper.Routing
{
    public class RouteProvider
    {
        public static IEndpointRouteBuilder Configure(IEndpointRouteBuilder routeBuilder)
        {
            #region Home

            routeBuilder.MapControllerRoute("Home", "Home", new { Controller = "Home", Action = "Index" });

            #endregion

            #region BlogPost

            routeBuilder.MapControllerRoute("BlogPost", "BlogPost", new { Controller = "BlogPost", Action = "Index" });

            #endregion

            #region BlogUser

            routeBuilder.MapControllerRoute("BlogUser", "BlogUser", new { Controller = "BlogUser", Action = "Index" });

            #endregion

            #region Blog Dummy User
            routeBuilder.MapControllerRoute("BlogDummyUser", "BlogDummyUser", new { Controller = "BlogDummyUser", Action = "Index" });
            #endregion
            return routeBuilder;
        }
    }
}
