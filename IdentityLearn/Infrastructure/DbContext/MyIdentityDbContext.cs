using IdentityLearn.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityLearn.Infrastructure.DbContext
{
    /// <summary>
    /// 创建EF的DbContext用来操作数据。在ASP.NET Identity中使用Code First方式来创建和管理数据库架构。自定义的DbContext必须继承自<see cref="IdentityDbContext&lt;T&gt;"/>
    /// </summary>
    public class MyIdentityDbContext:IdentityDbContext<MyUser>
    {
        /// <summary>
        /// MyIdentityDbContext构造函数，调用基类的构造函数并将数据库连接字符串的Name作为参数传递，用作连接数据库。
        /// </summary>
        public MyIdentityDbContext():base("IdentityDb")
        {

        }

        //public DbSet<MyUser> MyUsers { get; set; }
        //public DbSet<MyUserManager> MyUserManagers { get; set; }

        /// <summary>
        /// 在EntityFramework Code First成功创建数据库架构之后，MyIdentityDbContext静态构造函数调用Database.SetInitializer方法Seed数据库并且只执行一次
        /// </summary>
        static MyIdentityDbContext()
        {
            Database.SetInitializer<MyIdentityDbContext>(new IdentityDbInit());
        }

        /// <summary>
        /// Create方法将被OWIN Middleware回调并且返回MyIdentityDbContext实例，此实例被存储在OwinContext中
        /// </summary>
        /// <returns></returns>
        public static MyIdentityDbContext Create()
        {
            return new MyIdentityDbContext();
        }
    }

    /// <summary>
    /// MyIdentityDbContext初始化
    /// </summary>
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<MyIdentityDbContext>
    {
        /// <summary>
        /// 向数据库初始化数据
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(MyIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(MyIdentityDbContext context)
        {
            //初始化
        }
    }

}