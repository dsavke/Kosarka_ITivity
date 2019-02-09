using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
