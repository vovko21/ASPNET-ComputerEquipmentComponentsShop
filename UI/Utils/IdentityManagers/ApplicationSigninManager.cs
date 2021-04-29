using DAL.Entity.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace UI.Utils.IdentityManagers
{
    public class ApplicationSigninManager : SignInManager<User, string>
    {
        public ApplicationSigninManager(UserManager<User, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        { }

        public static ApplicationSigninManager Create(IdentityFactoryOptions<ApplicationSigninManager> options,
            IOwinContext owinContext)
        {
            var userManager = owinContext.GetUserManager<ApplicationUserManager>();
            var signinManager = new ApplicationSigninManager(userManager, owinContext.Authentication);

            return signinManager;
        }
    }
}