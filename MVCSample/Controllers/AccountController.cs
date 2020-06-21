using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCSample.Infrastructure.Services.Implementations;
using MVCSample.Infrastructure.Services.Interfaces;
using MVCSample.Models.ViewModels;

namespace MVCSample.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageService MessageService;
        
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMessageService messageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            MessageService = messageService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromQuery] MessageType messageType, AccountRegisterViewModel account)
        {
            //Inserted user account
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
                    MessageService.SendMessage(account.Email, messageType);
                    return RedirectToAction("Index", "Human");
                }
                task.Result
                    .Errors
                    .ToList()
                    .ForEach(x => ModelState.AddModelError(x.Code, x.Description));
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel login, [FromQuery] string returnUri)
        {
            if (ModelState.IsValid)
            {
                var sighInTask = _signInManager.PasswordSignInAsync(login.Email, login.Password, login.SavePassword, false);
                if (sighInTask.Result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUri))
                    {
                        return Redirect(returnUri);
                    }
                    return RedirectToAction("Index", "Human");
                }
                ModelState.AddModelError("", "Incorrect security pair!");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Human");
        }
    }
}
