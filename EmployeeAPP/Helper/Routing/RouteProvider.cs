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
            return routeBuilder;
        }
    }
}
