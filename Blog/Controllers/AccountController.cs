using Blog.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _usrMgr;
        private SignInManager<IdentityUser> _signMgr;

        public AccountController(UserManager<IdentityUser> userManger,SignInManager<IdentityUser> signInManager)
        {
            _usrMgr = userManger;
            _signMgr = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            var logingDto = new LoginDto { ReturnUrl=returnUrl };
            return View(logingDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _usrMgr.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "ایمیل یافت نشد ");
                    return View(loginDto);
                }
                Microsoft.AspNetCore.Identity.SignInResult result =await _signMgr.PasswordSignInAsync(user, loginDto.Password, false, false);
                if (result.Succeeded)
                    return Redirect(loginDto.ReturnUrl ?? "/");

                ModelState.AddModelError("", "ایمیل یا رمزعبور نادرست است ");
            }
            return View(loginDto);
        }

        public IActionResult Register(string returnUrl)
        {
            var logingDto = new LoginDto { ReturnUrl = returnUrl };
            return View(logingDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _usrMgr.FindByEmailAsync(loginDto.Email);
                if (user != null)
                {
                    ModelState.AddModelError("", "ایمیل قبلا وارد شده است");
                    return View(loginDto);
                }
                IdentityUser regUser = new IdentityUser { Email=loginDto.Email,UserName=loginDto.UserName };
                IdentityResult result =await _usrMgr.CreateAsync(regUser,loginDto.Password);
                if (result.Succeeded)
                {
                    var newlyUser = await _usrMgr.FindByEmailAsync(regUser.Email);
                   await _usrMgr.AddToRoleAsync(newlyUser, "user");
                    return Redirect(loginDto.ReturnUrl ?? "/");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }

            }
            return View(loginDto);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
           await _signMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
