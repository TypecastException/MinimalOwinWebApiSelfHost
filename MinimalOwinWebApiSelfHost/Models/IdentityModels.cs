using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace MinimalOwinWebApiSelfHost.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        //public async Task<ClaimsIdentity>
        //    GenerateUserIdentityAsync(ApplicationUserManager manager, string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

        //    // Add custom user claims here
        //    return userIdentity;
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }



    //public class ApplicationUserManager
    //    : UserManager<ApplicationUser, string>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser, string> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(
    //        IdentityFactoryOptions<ApplicationUserManager> options,
    //        IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(
    //            new ApplicationUserStore(new ApplicationDbContext()));

    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };

    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };

    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider =
    //                new DataProtectorTokenProvider<ApplicationUser>(
    //                    dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}


    //public class ApplicationRoleManager : RoleManager<IdentityRole>
    //{
    //    public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
    //        : base(roleStore)
    //    {
    //    }

    //    public static ApplicationRoleManager Create(
    //        IdentityFactoryOptions<ApplicationRoleManager> options,
    //        IOwinContext context)
    //    {
    //        return new ApplicationRoleManager(
    //            new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
    //    }
    //}


    //public class ApplicationUserStore
    //: UserStore<ApplicationUser>,
    //IDisposable
    //{
    //    public ApplicationUserStore()
    //        : this(new ApplicationDbContext())
    //    {
    //        base.DisposeContext = true;
    //    }

    //    public ApplicationUserStore(ApplicationDbContext context)
    //        : base(context)
    //    {
    //    }
    //}


    //public class ApplicationRoleStore
    //: RoleStore<IdentityRole>, IDisposable
    //{
    //    public ApplicationRoleStore()
    //        : base(new ApplicationDbContext())
    //    {
    //        base.DisposeContext = true;
    //    }

    //    public ApplicationRoleStore(ApplicationDbContext context)
    //        : base(context)
    //    {
    //    }
    //}

}
