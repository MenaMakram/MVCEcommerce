using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using MVCEcommerce.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCEcommerce.Startup))]
namespace MVCEcommerce
{
    public partial class Startup
    {
        ApplicationDbContext context;
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "Mathew";
                user.Email = "Mathew.m.nan@gmail.com";

                string userPWD = "Mathew123@";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Tager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Tager";
                roleManager.Create(role);
            }
        }
    }
}
