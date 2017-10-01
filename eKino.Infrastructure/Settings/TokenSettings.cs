namespace eKino.Infrastructure.Settings
{
    public class TokenSettings
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Expiry { get; set; }
    }
}