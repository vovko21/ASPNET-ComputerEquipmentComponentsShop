using DAL.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Utils;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpPost]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            var signInManager = HttpContext.GetOwinContext().GetUserManager<ApplicationSigninManager>();
            var resault = await signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
            //if (resault == SignInStatus.Success)
            //{
            //    signInManager.SignInAsync
            //}
            return View();
        }

        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signinManager = HttpContext.GetOwinContext().GetUserManager<ApplicationSigninManager>();

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            var identityResult = await manager.CreateAsync(user, model.Password);
            if (identityResult.Succeeded)
            {
                await signinManager.SignInAsync(user, false, false);
                return RedirectToAction("Index", "Games");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}