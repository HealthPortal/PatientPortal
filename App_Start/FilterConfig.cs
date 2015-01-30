using System.Web;
using System.Web.Mvc;
//using HealthPortal.App_Start;

namespace HealthPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new HandleErrorAttribute());
            
            
        }
    }
}