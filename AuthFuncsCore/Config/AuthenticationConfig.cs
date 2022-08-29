namespace AuthFuncsCore.Config
{
    public class AuthenticationConfig
    {
        public string JwtKey { get; set; }
        public int JwtExpiryDays { get; set; }
        public string JwtIssuer { get; set; }

    }
}
