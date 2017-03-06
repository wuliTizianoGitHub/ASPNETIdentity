using IdentityLearn.Infrastructure.DbContext;
using IdentityLearn.Models;
using Microsoft.AspNet.Identity;
using Owin;

namespace IdentityLearn
{
    //创建OWIN Startup 类

    public class IdentityConfig
    {
        //通过Katana（OWIN的实现）提供的API，将Middleware 中间件注册到Middleware中
        public void Configuration(IAppBuilder app)
        {
            //1.使用app.Use方法将IdentityFactoryMiddleware和参数callback回调函数注册到Owin Pipeline中
            //app.Use(typeof(IdentityFactoryMiddleware<T, IdentityFactoryOptions<T>>), args);
            //2.当IdentityFactoryMiddleware中间件被Invoke执行时，执行callback回调函数，返回具体实例Instance
            //TResult instance = ((IdentityFactoryMiddleware<TResult, TOptions>) this).Options.Provider.Create(((IdentityFactoryMiddleware<TResult, TOptions>) this).Options, context);
            //3.将返回的实例存储在Owin Context中
            //context.Set<TResult>(instance);


            //app.CreatePerOwinContext<MyIdentityDbContext>(MyIdentityDbContext.Create);
            app.CreatePerOwinContext(MyIdentityDbContext.Create);
            app.CreatePerOwinContext<MyUserManager>(MyUserManager.Create);
            app.CreatePerOwinContext<MyRoleManager>(MyRoleManager.Create);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Account/Login")
            });

            //通过CreatePerOwinContext方法将MyIdentityDbContext和MyUserManager的实例注册到OwinContext中。
            //这样确保每一次请求都能获取到相关ASP.NET Identity对象，而且还能保证全局唯一。
            //UseCookieAuthentication 方法指定了身份验证类型为ApplicationCookie，同时指定LoginPath属性，当Http请求内容认证不通过时重定向到指定的URL。
        }
    }
}