using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Service.Services;

[assembly: OwinStartup(typeof(Service.Startup))]

namespace Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Filters.Add(new CFExceptionFilter());


            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            ConfigureOAuth(app);
            config.Routes.MapHttpRoute("API Default Action", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            //config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            //config.Routes.MapHttpRoute("API loginCon", "api/{controller}/{name}/{pass}");
            config.Routes.MapHttpRoute("API Default", "api/{controller}");
            //config.MessageHandlers.Add(new BasicAuthenticationMessageHandler());
            //config.MessageHandlers.Add(new CompressHandler());
            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app)
        {

            var issuer = "http://localhost:59602";
            string audience = "099153c2625149bc8ecb3e85e03f0022";
            byte[] secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                  new JwtBearerAuthenticationOptions
                  {
                      AuthenticationMode = AuthenticationMode.Active,
                      AllowedAudiences = new[] { audience },
                      IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                      {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                      },
                      Provider = new OAuthBearerAuthenticationProvider
                      {
                          OnValidateIdentity = context =>
                          {
                              context.Ticket.Identity.AddClaim(new System.Security.Claims.Claim("newCustomClaim", "newValue"));
                              return Task.FromResult<object>(null);
                          }
                      }
                  });

        }
    }
}
