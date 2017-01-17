using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using PureDev.OAuth.Helpers;

namespace PureDev.OAuth.Google
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private const string AccessTokenUrl = "https://accounts.google.com/o/oauth2/token";
        private const string PeopleApiUrl = "https://www.googleapis.com/plus/v1/people/me/openIdConnect";

        private readonly OAuthSettings _oauthSettings;

        public GoogleAuthService(IOptions<OAuthSettings> oauthSettings)
        {
            _oauthSettings = oauthSettings.Value;
        }

        public GoogleAuthProfile Process(GoogleAuthViewModel vm)
        {
            var formData = new[]
            {
                new KeyValuePair<string, string>("code", vm.Code),
                new KeyValuePair<string, string>("client_id", vm.ClientId),
                new KeyValuePair<string, string>("client_secret", _oauthSettings.Google.Secret),
                new KeyValuePair<string, string>("redirect_uri", vm.RedirectUri),
                new KeyValuePair<string, string>("grant_type", vm.GrantType),
            };

            var resp = ApiClient.PostData<GoogleAuthResponse>(AccessTokenUrl, formData);
            return ApiClient.GetData<GoogleAuthProfile>(PeopleApiUrl, null, resp.AccessToken);
        }

        public bool ValidateRequestData(GoogleAuthViewModel vm)
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

    public interface IGoogleAuthService
    {
        GoogleAuthProfile Process(GoogleAuthViewModel vm);
        bool ValidateRequestData(GoogleAuthViewModel vm);
    }
}
