using IdentityLearn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Web;

namespace IdentityLearn.Infrastructure
{
    public static class HtmlStringHelper
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            MyUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<MyUserManager>();
            return new MvcHtmlString(userManager.FindByIdAsync(id).Result.UserName);
        }
    }
}
