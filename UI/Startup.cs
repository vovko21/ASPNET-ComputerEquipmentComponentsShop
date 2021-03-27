using DAL;
using DAL.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Data.Entity;
using UI.Utils;

[assembly: OwinStartup(typeof(UI.Startup))]
namespace UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DbContext>(() => new ApplicationContext());
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

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

            userManager.Create(new User()
            {
                Email = "admin@gmail.com",
                UserName = "admin"
            }, "admin");

            userManager.Create(new User()
            {
                Email = "user@gmail.com",
                UserName = "user"
            }, "user");
        }
    }
}
