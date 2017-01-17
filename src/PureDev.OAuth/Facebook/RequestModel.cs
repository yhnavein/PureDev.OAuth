using Newtonsoft.Json;

namespace PureDev.OAuth.Facebook
{
    public class FacebookAuthResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }

    public class FacebookAuthProfile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string DisplayName { get; set; }

        public GenericAuthProfile ToAuthProfile()
        {
            return new GenericAuthProfile
            {
                DisplayName = DisplayName,
                Email = Email,
                Id = Id,
                PictureUrl = $"https://graph.facebook.com/{Id}/picture?type=large"
            };
        }
    }

}
