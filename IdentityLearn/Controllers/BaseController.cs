﻿using IdentityLearn.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 存储在OwinContext中的UserManager实例
        /// </summary>
        protected MyUserManager UserManager
        {
            //通过CreatePerOwinContext方法将AppIdentityDbContext和 AppUserManager的实例注册到OwinContext后，可以通过OwinContext对象的Get方法来获取到他们
            //通过Microsoft.Owin.Host.SystemWeb 程序集，为HttpContext增加了扩展方法GetOwinContext，返回的 OwinContext对象是对Http请求的封装。
            //所以GetOwinContext方法可以获取到每一次Http请求的内容。接着通过IOwinContext的扩展方法GetUserManager获取到存储在OwinContext中的UserManager实例。
            get { return HttpContext.GetOwinContext().GetUserManager<MyUserManager>(); }
        }

        protected MyRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<MyRoleManager>(); }
        }

        /// <summary>
        /// 获取到存储在OwinContext中的Authentication实例，用来对用户进行验证操作
        /// </summary>
        protected IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
    }
}