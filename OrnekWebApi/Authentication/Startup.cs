using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(OrnekWebApi.Authentication.Startup))]
namespace OrnekWebApi.Authentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"), // token adresi
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),//10 Saat geçerli
                AllowInsecureHttp = true,
                Provider = new Saglayici() // Burada hata alırsanızz saglayıcı klasınızız doğru ayarladığınızdan emin olun.
            };

            
            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());// Bearer Authentication'ı kullanacağımızı belirttik.
        }
    }
}