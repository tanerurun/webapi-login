using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApi.StartUp))]
namespace WebApi
{
   
    public partial class StartUp //partial olması lazım
    {
         //burada iki method vardır 
         public void Configuration(IAppBuilder app)
        {
            this.Configuration(app);
        }

         public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new LoginProvider()
           
            });

        }
    }
}