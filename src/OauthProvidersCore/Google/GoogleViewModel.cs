namespace PureDev.OAuth.Google
{
    public class GoogleAuthViewModel
    {
        public string Code { get; set; }

        public string ClientId { get; set; }

        public string RedirectUri { get; set; }

        public string GrantType = "authorization_code";
    }

}