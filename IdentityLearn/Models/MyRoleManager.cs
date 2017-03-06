using IdentityLearn.Infrastructure.DbContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearn.Models
{
    public class MyRoleManager : RoleManager<MyRole>
    {
        public MyRoleManager(RoleStore<MyRole> store) : base(store)
        {
        }

        public static MyRoleManager Create(IdentityFactoryOptions<MyRoleManager> options, IOwinContext context)
        {
            return new MyRoleManager(new RoleStore<MyRole>(context.Get<MyIdentityDbContext>()));
        }
    }
}
