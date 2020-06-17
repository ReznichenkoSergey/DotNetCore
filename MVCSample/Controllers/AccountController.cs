using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSample.Models.ViewModels;

namespace MVCSample.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel account)
        {
            //User: user@gmail.com
            //Pass: Z123456z*
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = account.Email,
                    UserName = account.Email,
                };
                Task<IdentityResult> task = _userManager.CreateAsync(user, account.Password);
                if (task.Result.Succeeded)
                {
                    _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Human");
                }
                task.Result
                    .Errors
                    .ToList()
                    .ForEach(x => ModelState.AddModelError(x.Code, x.Description));
                
                //ViewData["errors"] = task.Result.Errors;
            }
            return View();
        }
    }
}
