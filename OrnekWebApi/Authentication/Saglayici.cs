using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
namespace OrnekWebApi.Authentication
{
    public class Saglayici : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); // Farklı domainlerden istek sorunu yaşamamak için

            //Burada kendi authentication yöntemimizi belirleyebiliriz.Veritabanı bağlantısı vs...
            if (context.UserName == "Sinan" && context.Password == "Bir")
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("name", context.UserName));
                identity.AddClaim(new Claim("role", "Admin"));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Geçersiz istek", "Hatalı kullanıcı bilgisi");
            }

            

        }
    }
}