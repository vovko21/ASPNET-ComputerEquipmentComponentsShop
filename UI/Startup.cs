using DAL;
using DAL.Entity.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Data.Entity;
using UI.Utils.IdentityManagers;

[assembly: OwinStartup(typeof(UI.Startup))]
namespace UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DbContext>(() => new ApplicationContext());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSigninManager>(ApplicationSigninManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            InitUsers();
        }

        private void InitUsers()
        {
            var userStore = new UserStore<User>(new ApplicationContext());
            var userManager = new ApplicationUserManager(userStore);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationContext()));

            var roleAdmin = new IdentityRole()
            {
                Name = "Admin"
            };

            var roleUser = new IdentityRole()
            {
                Name = "User"
            };

            roleManager.Create(roleAdmin);
            roleManager.Create(roleUser);

            var admin = new User()
            {
                Email = "admin@gmail.com",
                UserName = "admin"
            };

            var user = new User()
            {
                Email = "admin@gmail.com",
                UserName = "admin"
            };

            var adminChk = userManager.Create(admin, password: "111111");
            var userChk = userManager.Create(user, password: "123456");

            if (adminChk.Succeeded == true)
            {
                userManager.AddToRole(admin.Id, "Admin");
            }
            if (userChk.Succeeded == true)
            {
                userManager.AddToRole(user.Id, "User");
            }
        }
    }
}
