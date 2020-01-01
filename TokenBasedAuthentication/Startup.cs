using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using TokenBasedAuthentication.Models;

[assembly: OwinStartup(typeof(TokenBasedAuthentication.Startup))]
namespace TokenBasedAuthentication
{
    public class Startup
    {
            public void Configuration(IAppBuilder app)
            {
                // Enable CORS (cross origin resource sharing) for making request using browser from different domains
                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    //The Path For generating the Toekn
                    TokenEndpointPath = new PathString("/token"),
                    //Setting the Token Expired Time (24 hours)
                    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                    //AuthorizationServerProvider class will validate the user credentials
                    Provider = new AuthorizationServerProvider()
                };
                //Token Generations
                app.UseOAuthAuthorizationServer(options);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

                HttpConfiguration config = new HttpConfiguration();
                WebApiConfig.Register(config);
            }
        }
    }