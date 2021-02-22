namespace LuizMario.Dto.Configuration
{
    public class TokenConfigurationDto
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public int RefreshTokenSeconds { get; set; }
    }
}
