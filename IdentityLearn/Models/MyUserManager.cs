using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentityLearn.Infrastructure.DbContext;

namespace IdentityLearn.Models
{
    /// <summary>
    /// 用于管理MyUser类，必须继承自<see cref="UserManager&lt;T&gt;"/>,此处的T为<see cref="MyUser"/>
    /// <para><see cref="UserManager&lt;T&gt;"/>提供了创建和操作用户的一些基本方法并且全面支持C# 异步编程</para>
    /// </summary>
    public class MyUserManager:UserManager<MyUser>
    {
        /// <summary>
        /// 这里不直接通过EF操作数据,而是使用<see cref="IUserStore&lt;T&gt;"/>
        /// </summary>
        /// <param name="store"></param>
        public MyUserManager(IUserStore<MyUser> store)
         : base(store) {
        }

        /// <summary>
        /// 返回<see cref="MyUserManager"/>的实例，用来操作和管理用户。它需要传入OwinContext对象，通过该上下文对象，获取到存储在Owin环境字典中的Database Context实例。
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static MyUserManager Create(
             IdentityFactoryOptions<MyUserManager> options,
             IOwinContext context)
        {
            MyIdentityDbContext DbContext = context.Get<MyIdentityDbContext>();
            MyUserManager manager = new MyUserManager(new UserStore<MyUser>(DbContext));

            return manager;
        }
    }
}