using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearn.Models.ViewModel
{
    public class RoleEditModel
    {
        public MyRole Role { get; set; }
        public IEnumerable<MyUser> Members { get; set; }
        public IEnumerable<MyUser> NonMembers { get; set; }
    }
}
