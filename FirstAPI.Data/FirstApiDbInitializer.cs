using FirstAPI.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAPI.Data
{
    public class FirstApiDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //base.Seed(context);
            SeedRoles(context);
            SeedUsers(context);
        }

        public void SeedRoles(ApplicationDbContext context)
        {
            List<IdentityRole> rolesInThisApplication = new List<IdentityRole>();

            rolesInThisApplication.Add(new IdentityRole() { Name = "Administrator" });
            rolesInThisApplication.Add(new IdentityRole() { Name = "Moderator" });
            rolesInThisApplication.Add(new IdentityRole() { Name = "User" });

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (IdentityRole role in rolesInThisApplication)
            {
                if (!roleManager.RoleExists(role.Name))
                {
                    var result = roleManager.Create(role);

                    if (result.Succeeded) continue;
                }
            }
        }

        public void SeedUsers(ApplicationDbContext context)
        {
            var usersStore = new UserStore<ApplicationUser>(context);
            var usersManager = new UserManager<ApplicationUser>(usersStore);

            ApplicationUser admin = new ApplicationUser();
            admin.Email = "admin@gmail.com";
            admin.UserName = "admin@gmail.com";
            var password = "Admin@12345";

            if (usersManager.FindByEmail(admin.Email) == null)
            {
                var result = usersManager.Create(admin, password);

                if (result.Succeeded)
                {
                    usersManager.AddToRole(admin.Id, "Administrator");
                    usersManager.AddToRole(admin.Id, "Moderator");
                    usersManager.AddToRole(admin.Id, "User");
                }
            }
        }

    }
}
