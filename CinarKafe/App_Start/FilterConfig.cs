using System.Web;
using System.Web.Mvc;

namespace CinarKafe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // for authorizations
            filters.Add(new AuthorizeAttribute());

            // for secure connections
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
