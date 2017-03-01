using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityLearn.Models
{
    /// <summary>
    /// 自定义User类，继承自Microsoft.AspNet.Identity.EntityFramework下的<see cref="IdentityUser"/>类，提供了基本的用户信息
    /// </summary>
    public class MyUser:IdentityUser
    {
        //TODO：这里添加自定义的额外用户信息
    }
}