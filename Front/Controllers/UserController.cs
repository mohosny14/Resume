using Front.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class UserController : Controller
    {
        UserManager<IdentityUser> UserManager;
        SignInManager<IdentityUser> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        public UserController(UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> _SignInManager,
              RoleManager<IdentityRole> _RoleManager)
        {
            UserManager = userManager;
            SignInManager = _SignInManager;
            RoleManager = _RoleManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            else
            {
                var result =
                await SignInManager.PasswordSignInAsync(model.UserName, model.Password,
                        model.RememberMe, true);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Your Data Is Not Valid");
                    return View();
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Invalid User Name Or password");
                    return View();
                }
                else
                {


                    return RedirectToAction("Index", "Projects");
                }
            }

        }
    }
}
