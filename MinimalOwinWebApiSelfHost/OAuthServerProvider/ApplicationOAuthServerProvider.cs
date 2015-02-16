using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add Usings:
using Microsoft.Owin.Security;
﻿using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using MinimalOwinWebApiSelfHost.Models;

// Add to use Identity:
using Microsoft.AspNet.Identity.Owin;

namespace MinimalOwinWebApiSelfHost.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider 
        : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(
            OAuthValidateClientAuthenticationContext context)
        {
            // This call is required...
            // but we're not using client authentication, so validate and move on...
            await Task.FromResult(context.Validated());
        }


        public override async Task GrantResourceOwnerCredentials(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            // ** Use extension method to get a reference to the user manager from the Owin Context:
            var manager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            // UserManager allows us to retrieve use with name/password combo:
            var user = await manager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError(
                    "invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            // Add claims associated with this user to the ClaimsIdentity object:
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            foreach (var userClaim in user.Claims)
            {
                identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }

            context.Validated(identity);
        }
    }
}
