using Microsoft.Extensions.DependencyInjection;
using PureDev.OAuth.Facebook;
using PureDev.OAuth.Google;

namespace PureDev.OAuth
{
    public static class OAuthStartup
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IGoogleAuthService, GoogleAuthService>();
            services.AddTransient<IFacebookAuthService, FacebookAuthService>();
        }
    }
}
