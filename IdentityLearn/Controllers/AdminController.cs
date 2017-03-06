using IdentityLearn.Models;
using IdentityLearn.Models.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdentityLearn.Controllers
{
    [Authorize(Roles = "Administrator")] //只有角色是Administrator的用户才能进入此模块
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MyUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                MyUser user = new MyUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrorsFromResult(result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            MyUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                if (user.UserName == "Admin")
                {
                    return View("Error", new[] { "请勿删除管理员！" });
                }

                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View("Error", result.Errors);
            }
            return View("Error", new[] { "User Not Found" });
        }

        public async Task<ActionResult> Edit(string id)
        {
            MyUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            //根据Id找到MyUser对象
            MyUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                if (user.UserName == "Admin")
                {
                    return View("Error", new[] { "请勿修改管理员密码！" });
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    //验证密码是否满足要求
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                //验证Email是否满足要求
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "无法找到改用户");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}