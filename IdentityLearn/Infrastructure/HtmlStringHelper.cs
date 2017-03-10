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
using System.Reflection;

namespace IdentityLearn.Infrastructure
{
    public static class HtmlStringHelper
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {

            MyUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<MyUserManager>();
            return new MvcHtmlString(userManager.FindByIdAsync(id).Result.UserName);
        }

        /// <summary>
        /// 格式化ClaimType的值
        /// </summary>
        /// <param name="html"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static MvcHtmlString ClaimType(this HtmlHelper html, string claimType)
        {
            FieldInfo[] fields = typeof(ClaimTypes).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(null).ToString() == claimType)
                {
                    return new MvcHtmlString(field.Name);
                }
            }
            return new MvcHtmlString(string.Format("{0}",
            claimType.Split('/', '.').Last()));
        }
    }
}
