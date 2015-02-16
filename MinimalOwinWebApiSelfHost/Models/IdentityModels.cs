using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add using statements:
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MinimalOwinWebApiSelfHost.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        public ApplicationUser(string email) : base(email)
        {
            // Use the email for both user name AND email:
            UserName = email;
        }
    }


    public class ApplicationUserManager 
        : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store) { }


        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            return new ApplicationUserManager(
                new UserStore<ApplicationUser>(
                    context.Get<ApplicationDbContext>()));
        }
    }
}
