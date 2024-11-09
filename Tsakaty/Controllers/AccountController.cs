using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tsakaty.Models;
using Tsakaty.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tsakaty.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager
                ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        public async Task<IActionResult> SaveRegister(UserRegisterViewModel userRegisterViewModel)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser appUser =new ApplicationUser();
                appUser.Email = userRegisterViewModel.Email;
                appUser.UserName=userRegisterViewModel.Name;
                appUser.Address = userRegisterViewModel.Address;

                IdentityResult result= await userManager.CreateAsync(appUser,userRegisterViewModel.Password);
                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(appUser,false);
                    return RedirectToAction("Index", "Task");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("Register",userRegisterViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login",controllerName:"Account");
        }
        public IActionResult Login()
        {
            return View("Login");
        }

        public async Task<IActionResult> SaveLogin(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user= await userManager.FindByEmailAsync(userLoginViewModel.Email);
                if(user!=null)
                {
                    var ValidPassword =await userManager.CheckPasswordAsync(user, userLoginViewModel.Password);
                    if (ValidPassword)
                    {
                        await signInManager.SignInAsync(user, userLoginViewModel.RemenberMe);
                        return RedirectToAction("Index", "Task");
                    }
                    ModelState.AddModelError("", "Email Or Password is Wrong !!");
                }
                ModelState.AddModelError("", "Email Or Password is Wrong !!");
                return View("Login",userLoginViewModel);
            }
            return View("Login", userLoginViewModel);
        }
    }
}
