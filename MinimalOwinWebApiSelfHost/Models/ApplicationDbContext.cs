using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add using:
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;

namespace MinimalOwinWebApiSelfHost.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("MyDatabase")
        {

        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public IDbSet<Company> Companies { get; set; }
        public IDbSet<MyUser> Users { get; set; }
        public IDbSet<MyUserClaim> Claims { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<User>().HasMany<UserRole>((User u) => u.Claims);
        //    modelBuilder.Entity<UserRole>().HasKey((UserRole r) =>
        //        new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    EntityTypeConfiguration<MyUserClaim> table1 = modelBuilder.Entity<MyUserClaim>().ToTable("MyUserClaims");
        //    table1.HasRequired<MyUser>((MyUserClaim u) => u.User);
        //}

    }


    public class ApplicationDbInitializer 
        : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected async override void Seed(ApplicationDbContext context)
        {
            context.Companies.Add(new Company { Name = "Microsoft" });
            context.Companies.Add(new Company { Name = "Apple" });
            context.Companies.Add(new Company { Name = "Google" });
            context.SaveChanges();

            var john = new MyUser { Email = "john@example.com" };
            var jimi = new MyUser { Email = "jimi@Example.com" };

            john.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = john.Id, ClaimValue = john.Email });
            john.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = john.Id, ClaimValue = "Admin" });

            jimi.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = jimi.Id, ClaimValue = jimi.Email });
            jimi.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = john.Id, ClaimValue = "User" });

            var store = new MyUserStore(context);
            await store.AddUserAsync(john, "JohnsPassword");
            await store.AddUserAsync(jimi, "JimisPassword");

        }
    }
}
