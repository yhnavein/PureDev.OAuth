using Newtonsoft.Json;

namespace PureDev.OAuth.Google
{
    public class GoogleAuthResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "id_token")]
        public string IdToken { get; set; }
    }

    public class GoogleAuthProfile
    {
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "sub")]
        public string Sub { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "given_name")]
        public string GivenName { get; set; }

        [JsonProperty(PropertyName = "family_name")]
        public string FamilyName { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public string ProfileUrl { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string PictureUrl { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "email_verified")]
        public bool EmailVerified { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }


        public GenericAuthProfile ToAuthProfile()
        {
            return new GenericAuthProfile
            {
                DisplayName = DisplayName,
                Email = Email,
                Id = Sub,
                PictureUrl = PictureUrl
            };
        }
    }

}
