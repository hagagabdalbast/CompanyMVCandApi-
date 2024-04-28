using Demo.Models;
using Demo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                //mapping data from vm to model
                userModel.UserName = newUserVM.UserName;
                userModel.Email = newUserVM.Email;
                userModel.PasswordHash = newUserVM.Password;
                //save in db
                IdentityResult result=await userManager.CreateAsync(userModel,newUserVM.Password);

                if (result.Succeeded)
                {
                    //AddRole to db
                    //await userManager.AddToRoleAsync(userModel, "Student");
                    //create cookies
                    await signInManager.SignInAsync(userModel, isPersistent: false);

                   return RedirectToAction("Index", "Employee");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }
                
            }
            return View(newUserVM);
        }

        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //for any hack cant reach to your data
        public async Task<IActionResult> Login(LoginViewModel userVM)
        {
            //check
            if (ModelState.IsValid)
            {
                ApplicationUser userModel =await userManager.FindByNameAsync(userVM.UserName);
                if(userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found == true)
                    {
                        //create cookies
                        await signInManager.SignInAsync(userModel, userVM.RememberMe);
                        return RedirectToAction("Index", "Employee");
                    }

                }
                ModelState.AddModelError("", "UserName or Password error");

            }

            return View(userVM);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                //mapping data from vm to model
                userModel.UserName = newUserVM.UserName;
                userModel.Email = newUserVM.Email;
                userModel.PasswordHash = newUserVM.Password;
                //save in db
                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);

                if (result.Succeeded)
                {
                    //AddRole to db
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    //creat cookies
                    await signInManager.SignInAsync(userModel, isPersistent: false);

                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    foreach (var errorItem in result.Errors)
                    {
                        ModelState.AddModelError("Password", errorItem.Description);
                    }
                }

            }
            return View(newUserVM);
        }

            }
}
