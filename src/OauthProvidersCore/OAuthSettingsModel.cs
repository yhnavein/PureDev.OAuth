using Newtonsoft.Json;

namespace PureDev.OAuth
{
    public class OAuthSettings
    {
        public OAuthProviderSettings Facebook { get; set; }

        public OAuthProviderSettings Google { get; set; }
    }

    public class OAuthProviderSettings
    {
        public string ClientId { get; set; }

        [JsonIgnore]
        public string Secret { get; set; }

        public string Url { get; set; }
    }

}
