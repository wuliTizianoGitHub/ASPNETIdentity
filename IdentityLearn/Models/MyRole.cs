using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearn.Models
{
    public class MyRole : IdentityRole
    {

        public MyRole() : base() { }
        public MyRole(string name) : base(name) { }
        // 在此添加额外属性

    }
}
