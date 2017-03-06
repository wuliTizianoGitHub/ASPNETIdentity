using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearn.Models.ViewModel
{
    public class RoleModificationModel
    {
        public string RoleName { get; set; }
        public string[] IDsToAdd { get; set; }
        public string[] IDsToDelete { get; set; }
    }
}
