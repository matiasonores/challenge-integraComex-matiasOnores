using System.Web;
using System.Web.Mvc;

namespace challenge_integraComex_matiasOnores
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
