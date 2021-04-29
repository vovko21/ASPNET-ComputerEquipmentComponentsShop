using DAL.Entity;
using DAL.Entity.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;

namespace UI.Utils.IdentityManagers
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        { }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext owinContext)
        {
            var dbContext = owinContext.Get<DbContext>();
            var manager = new ApplicationUserManager(new UserStore<User>(dbContext));

            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                //RequireDigit = true,
                //RequireUppercase = true,
                //RequireNonLetterOrDigit = true
            };

            return manager;
        }
    }
}