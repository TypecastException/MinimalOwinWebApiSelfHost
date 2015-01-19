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
            // DEMO ONLY: Pretend we are doing some sort of REAL checking here:
            if (context.Password != "password")
            {
                context.SetError(
                    "invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }

            // Create or retrieve a ClaimsIdentity to represent the 
            // Authenticated user:
            ClaimsIdentity identity = 
                new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("user_name", context.UserName));

            // Identity info will ultimatly be encoded into an Access Token
            // as a result of this call:
            context.Validated(identity);
        }
    }
}
