using System.Collections.Generic;
using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    public class HomeController : BaseController
    {

        [Authorize]//开启登录验证
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        private Dictionary<string, object> GetData(string actionName)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("视图",actionName);
            dict.Add("用户",HttpContext.User.Identity.Name);
            dict.Add("是否身份验证",HttpContext.User.Identity.IsAuthenticated);
            dict.Add("身份验证类型",HttpContext.User.Identity.AuthenticationType);
            dict.Add("是否隶属于Administrator", HttpContext.User.IsInRole("Administrator"));
            return dict;
        }
    }
}