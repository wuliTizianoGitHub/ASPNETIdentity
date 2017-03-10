using IdentityLearn.Infrastructure;
using IdentityLearn.Models;
using IdentityLearn.Models.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
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
                  
                    var claimsIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //将自定义声明传入到ClaimsIndentity
                    claimsIdentity.AddClaims(LocationClaimsProvider.GetClaims(claimsIdentity));
                    claimsIdentity.AddClaims(ClaimsRoles.CreateRolesFromClaims(claimsIdentity));

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


        [HttpPost]
        [AllowAnonymous]
        public ActionResult GoogleLogin(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback",
                new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }

        /// <summary>
        /// Google登陆成功后（即授权成功）回掉此Action
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();
            MyUser user = await UserManager.FindAsync(loginInfo.Login);
            if (user == null)
            {
                user = new MyUser
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,
                    City = Cities.Shanghai,
                    Country = Countries.China
                };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
            }
            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);
            ident.AddClaims(loginInfo.ExternalIdentity.Claims);
            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);
            return Redirect(returnUrl ?? "/");
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