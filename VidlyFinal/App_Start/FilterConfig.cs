using System.Web.Mvc;

namespace VidlyExercice1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // Global filter - Authentication required for any page of the app
            filters.Add(new AuthorizeAttribute());
            // Add Https Attribute to only allow https connection
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
