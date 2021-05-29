using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

[assembly: OwinStartup(typeof(DemoApp.App_Start.Startup))]
namespace DemoApp.App_Start
{

	public partial class Startup
	{
		private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string authority = EnsureTrailingSlash(ConfigurationManager.AppSettings["ida:Authority"]);
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["ida:PostLogoutRedirectUri"];
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
		public void ConfigureAuth(IAppBuilder app)
		{
			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

			app.UseCookieAuthentication(new CookieAuthenticationOptions());

			app.UseOpenIdConnectAuthentication(
			new OpenIdConnectAuthenticationOptions
			{
				ClientId = clientId,
				Authority = authority,
				//Added by deepak
				//RequireHttpsMetadata=false,
				PostLogoutRedirectUri = postLogoutRedirectUri,

				Notifications = new OpenIdConnectAuthenticationNotifications()
				{
					AuthenticationFailed = (context) =>
					{
						return System.Threading.Tasks.Task.FromResult(0);
					},
					SecurityTokenValidated = (context) =>
					{
						var claims = context.AuthenticationTicket.Identity.Claims;
						var groups = from c in claims
									 where c.Type == "groups"
									 select c;

						foreach (var group in groups)
						{
							context.AuthenticationTicket.Identity.AddClaim(new Claim(ClaimTypes.Role, group.Value));
						}
						return Task.FromResult(0);
					}
				}

			}
			);

			// This makes any middleware defined above this line run before the Authorization rule is applied in web.config
			app.UseStageMarker(PipelineStage.Authenticate);
		}

		private static string EnsureTrailingSlash(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}

			if (!value.EndsWith("/", StringComparison.Ordinal))
			{
				return value + "/";
			}

			return value;
		}
		
	}
}