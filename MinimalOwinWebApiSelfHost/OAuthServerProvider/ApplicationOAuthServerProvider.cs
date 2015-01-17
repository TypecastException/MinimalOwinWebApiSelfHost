using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// GetUserManager Extension lives HERE:
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
﻿using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using MinimalOwinWebApiSelfHost.Models;


namespace MinimalOwinWebApiSelfHost.OAuthServerProvider
{
    public class ApplicationOAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // This call is required...
            // but we're not using client authentication, so validate and move on...
            await Task.FromResult(context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context.Password != "password")
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                context.Rejected();
                return;
            }
            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim("user_name", context.UserName));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            //IDictionary<string, string> authProperties = new Dictionary<string, string>
            //{
            //    { "userName", context.UserName },
            //    {"role", "Admin"}
            //};

            //AuthenticationProperties properties = new AuthenticationProperties(authProperties);
            //var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            //context.Validated(ticket);
            context.Validated(oAuthIdentity);
        }
    }
}
