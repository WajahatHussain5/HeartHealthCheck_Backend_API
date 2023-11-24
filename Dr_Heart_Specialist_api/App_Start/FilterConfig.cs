using System.Web;
using System.Web.Mvc;

namespace Dr_Heart_Specialist_api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
