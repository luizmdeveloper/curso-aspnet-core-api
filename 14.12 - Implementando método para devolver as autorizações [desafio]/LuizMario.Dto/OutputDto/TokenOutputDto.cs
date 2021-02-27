using Newtonsoft.Json;

namespace LuizMario.Dto.OutputDto
{
    public class TokenOutputDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
