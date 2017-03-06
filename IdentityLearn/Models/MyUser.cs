using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityLearn.Models
{
    /// <summary>
    /// 自定义User类，继承自Microsoft.AspNet.Identity.EntityFramework下的<see cref="IdentityUser"/>类，提供了基本的用户信息
    /// </summary>
    public class MyUser:IdentityUser
    {
        //TODO：这里添加自定义的额外用户信息
        public Countries Country { get; set; }
        public Cities City { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch (city)
            {
                case Cities.Shanghai:
                case Cities.Hangzhou:
                    Country = Countries.China;
                    break;
                case Cities.NewYork:
                    Country = Countries.USA;
                    break;
                case Cities.Tokyo:
                    Country = Countries.Japan;
                    break;
                default:
                    Country = Countries.None;
                    break;
            }
        }
    }
    public enum Countries
    {
        China,
        USA,
        Japan,
        None
    }
    public enum Cities
    {
        Shanghai,
        Hangzhou,
        NewYork,
        Tokyo
    }

}