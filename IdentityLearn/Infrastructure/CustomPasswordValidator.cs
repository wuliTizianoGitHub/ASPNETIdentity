using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearn.Infrastructure
{
    public class CustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);
            if (password.Contains("123456"))
            {
                List<string> errors = result.Errors.ToList();
                errors.Add("密码不能包含连续数字");
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}
