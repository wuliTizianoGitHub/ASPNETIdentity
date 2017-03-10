using IdentityLearn.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace IdentityLearn
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomAuthorizeAttribute());
        }
    }
}
