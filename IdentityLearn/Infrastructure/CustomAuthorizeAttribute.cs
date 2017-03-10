using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdentityLearn.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string Issuer { get; set; }

        public string ClaimType { get; set; }

        public string Value { get; set; }

        public new string[] Roles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isTrue = true;

            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }

            if (!httpContext.User.Identity.IsAuthenticated ||
                !(httpContext.User.Identity is ClaimsIdentity) ||
                !((ClaimsIdentity)httpContext.User.Identity).HasClaim(x =>x.Issuer == Issuer && x.Type == ClaimType && x.Value == Value))
            {
                isTrue = false;
            }
            else
            {
                if (Roles != null && !Roles.Any(httpContext.User.IsInRole))
                {
                    httpContext.Response.StatusCode = 403;
                    isTrue = false;
                }
               
            }
            return isTrue;
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.Response.StatusCode == 403)
            {
                filterContext.Result = new RedirectResult("/Home/Error");
            }
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}