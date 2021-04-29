using DAL.Entity.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models;
using UI.Utils.IdentityManagers;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSigninManager SignInManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationSigninManager>();
        }
        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }
        private IAuthenticationManager AuthenticationManager
        {
            get => HttpContext.GetOwinContext().Authentication;
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.EmailViewModel
            };

            try
            {
                IdentityResult identityResult = await UserManager.CreateAsync(user, model.Password);
                UserManager.AddToRole(user.Id, "User");
                if (identityResult.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false, false);
                    return RedirectToAction("Index", "ComputerComponent");
                }
            }
            catch (DbEntityValidationException e)
            {
                string message = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    message += $"Entity of type {eve.Entry.Entity.GetType()} has validation errors: \n";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += $"- Property: {ve.PropertyName}, Error: {ve.ErrorMessage}";
                    }
                }
                throw new DbEntityValidationException(message);
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindAsync(model.Login, model.Password);
            if (user == null)
            {
                AddError("Uncorrect login or password");
            }
            else
            {
                var resault = await SignInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe == "on", false);

                if (resault == SignInStatus.Success)
                {
                    AuthenticationManager.SignOut();

                    await SignInManager.SignInAsync(user, false, false);

                    return RedirectToAction("Index", "ComputerComponent");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                AuthenticationManager.SignOut();
                return RedirectToAction("Index", "ComputerComponent");
            }
            return RedirectToAction("Login");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private void AddError(string message)
        {
            ModelState.AddModelError("", message);
        }
    }
}