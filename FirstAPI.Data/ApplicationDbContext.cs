using FirstAPI.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>   //DbContext
    {
        public ApplicationDbContext() : base("name=FirstAPIConnection")
        {
            //for create pre set user
            Database.SetInitializer<ApplicationDbContext>(new FirstApiDbInitializer());
        }

        public DbSet<Employee> Employees { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
