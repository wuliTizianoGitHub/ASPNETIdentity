using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    public class ClaimsController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            ClaimsIdentity claim = HttpContext.User.Identity as ClaimsIdentity;
            if (claim == null)
            {
                return View("Error", new string[] { "未找到声明" });
            }
            else
            {
                return View(claim.Claims);
            }
        }
    }
}