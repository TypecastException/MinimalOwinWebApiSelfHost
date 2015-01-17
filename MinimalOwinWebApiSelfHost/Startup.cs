using System;

// Add the following usings:
using Owin;
using System.Web.Http;
using MinimalOwinWebApiSelfHost.Models;
using MinimalOwinWebApiSelfHost.OAuthServerProvider;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;


namespace MinimalOwinWebApiSelfHost
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {


            ConfigureIdentity(app);
            var builder = (Microsoft.Owin.Builder.AppBuilder)app;

            var webApiConfiguration = ConfigureWebApi();
            app.UseWebApi(webApiConfiguration);

        }


        private void ConfigureIdentity(IAppBuilder app)
        {
            //// Configure the db context and user manager to use a single instance per request
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                // Only do this for demo!!
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }


        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            return config;
        }
    }
}
