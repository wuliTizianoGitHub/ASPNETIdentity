using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    public class HomeController : BaseController
    {

        [Authorize]//开启登录验证
        public ActionResult Index()
        {
            return View();
        }
    }
}