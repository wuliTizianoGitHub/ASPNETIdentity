using IdentityLearn.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace IdentityLearn.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Infrastructure.DbContext.MyIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Infrastructure.DbContext.MyIdentityDbContext";
        }

        protected override void Seed(Infrastructure.DbContext.MyIdentityDbContext context)
        {
            MyUserManager userManager = new MyUserManager(new UserStore<MyUser>(context));
            MyRoleManager roleManager = new MyRoleManager(new RoleStore<MyRole>(context));

            string roleName = "Administrator";
            string userName = "Admin";
            string password = "Password2015";
            string email = "admin@jkxy.com";

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new MyRole(roleName));
            }

            MyUser user = userManager.FindByName(userName);
            if (user == null)
            {
                userManager.Create(new MyUser { UserName = userName, Email = email },
                    password);
                user = userManager.FindByName(userName);
            }

            if (!userManager.IsInRole(user.Id, roleName))
            {
                userManager.AddToRole(user.Id, roleName);
            }
            foreach (MyUser dbUser in userManager.Users)
            {
                if (dbUser.Country == Countries.None)
                {
                    dbUser.SetCountryFromCity(dbUser.City);
                }
            }
            context.SaveChanges();
        }
    }
}