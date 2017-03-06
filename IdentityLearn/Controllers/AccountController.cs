using IdentityLearn.Models;
using IdentityLearn.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    public class AccountController: BaseController
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(MyUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new MyUser { UserName=model.Name, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user,model.Password);

                if (result.Succeeded)
                {
                    return Redirect("/Home/Index");
                }
                AddErrorsFromResult(result);
            }

            return View(model);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="returnUrl">登录成功后的重定向地址</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]   //跳过Authorize身份验证
        public ActionResult Login(string returnUrl)
        {
            //判断用户是否已经登录
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "您已经登录！" });
            }

            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] //用来防止CSRF跨站请求伪造
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //根据用户名和密码查询到用户
                MyUser user = await UserManager.FindAsync(model.Name, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "无效的用户名或密码");
                }
                else
                {
                    //创建Cookie 并输出到客户端浏览器，这样浏览器的下一次请求就会带着这个Cookie;
                    //当请求经过AuthenticateRequest 阶段时，读取并解析Cookie
                    //1.创建用来代表当前登录用户的ClaimsIdentity 对象，ClaimsIndentity 是 ASP.NET Identity 中的类，它实现了IIdentity 接口。
                    //ClaimsIdentity 对象实际上由AppUserManager 对象的CreateIdentityAsync 方法创建。
                    //它需要接受一个MyUser 对象和身份验证类型，在这儿选择ApplicationCookie。
                    var claimsIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //2.
                    //claimsIdentity.AddClaims(LocationClaimsProvider.GetClaims(claimsIdentity));
                    //claimsIdentity.AddClaims(ClaimsRoles.CreateRolesFromClaims(claimsIdentity));

                    AuthManager.SignOut();
                    //AuthenticationProperties 对象和ClaimsIdentity 对象，AuthticationProperties 有众多属性，我在这儿只设置IsPersistent=true 。
                    //意味着Authentication Session 被持久化保存，当开启新Session 时，该用户不必重新验证
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = false }, claimsIdentity);

                    //登录成功重定向到原请求地址
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}