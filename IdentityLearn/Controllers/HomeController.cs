using IdentityLearn.Infrastructure;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    public class HomeController : BaseController
    {

        [CustomAuthorize]//开启登录验证
        public ActionResult Index()
        {
            return View(GetData("Index"));
        }

        [CustomAuthorize(Roles = new string[] { "Employee" })]
        public ActionResult OtherAction()
        {
            return Content("没有页面");
            //return View("Error", new string[] { "没有权限！" });
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

        public ActionResult Error()
        {
            return View();
        }
    }
}