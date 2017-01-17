using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using PureDev.OAuth.Helpers;

namespace PureDev.OAuth.Facebook
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly string[] GraphFields = { "id", "email", "first_name", "last_name", "link", "name" };
        private const string AccessTokenUrl = "https://graph.facebook.com/v2.5/oauth/access_token";
        private const string GraphApiBaseUrl = "https://graph.facebook.com/v2.5/me?fields=";

        private readonly OAuthSettings _oauthSettings;

        public FacebookAuthService(IOptions<OAuthSettings> oauthSettings)
        {
            _oauthSettings = oauthSettings.Value;
        }

        public FacebookAuthProfile Process(FacebookAuthViewModel vm)
        {
            var graphApiUrl = GraphApiBaseUrl + string.Join(",", GraphFields);
            var formData = new[]
            {
                new KeyValuePair<string, string>("code", vm.Code),
                new KeyValuePair<string, string>("client_id", vm.ClientId),
                new KeyValuePair<string, string>("client_secret", _oauthSettings.Facebook.Secret),
                new KeyValuePair<string, string>("redirect_uri", vm.RedirectUri),
            };

            var resp = ApiClient.GetData<FacebookAuthResponse>(AccessTokenUrl, formData);
            return ApiClient.GetData<FacebookAuthProfile>(graphApiUrl, null, resp.AccessToken);
        }

        public bool ValidateRequestData(FacebookAuthViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Code))
                throw new Exception("Code cannot be empty");
            if (string.IsNullOrEmpty(vm.ClientId))
                throw new Exception("ClientId cannot be empty");
            if (string.IsNullOrEmpty(vm.RedirectUri))
                throw new Exception("RedirectUri cannot be empty");

            return true;
        }
    }

    public interface IFacebookAuthService
    {
        FacebookAuthProfile Process(FacebookAuthViewModel vm);
        bool ValidateRequestData(FacebookAuthViewModel vm);
    }
}
