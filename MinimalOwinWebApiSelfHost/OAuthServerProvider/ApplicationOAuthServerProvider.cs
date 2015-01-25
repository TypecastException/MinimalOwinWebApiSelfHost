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
            // Retrieve user from database:
            var store = new MyUserStore(new ApplicationDbContext());
            var user = await store.FindByEmailAsync(context.UserName);

            // Validate user/password:
            if(user == null || !store.PasswordIsValid(user, context.Password))
            {
                context.SetError(
                    "invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            // Add claims associated with this user to the ClaimsIdentity object:
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            foreach(var userClaim in user.Claims)
            {
                identity.AddClaim(new Claim(userClaim.ClaimType, userClaim.ClaimValue));
            }
            
            context.Validated(identity);
        }
    }
}
