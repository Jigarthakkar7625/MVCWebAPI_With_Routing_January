using System.Web;
using System.Web.Mvc;

namespace MVCWebAPI_January
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
