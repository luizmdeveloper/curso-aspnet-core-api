using System.ComponentModel.DataAnnotations;

namespace LuizMario.Dto.Input
{
    public class RefreshTokenInputDto
    {
        [Required(ErrorMessage = "RefreshToken é obrigatório")]
        public string RefreshToken { get; set; }

        [Required(ErrorMessage = "GrantType é obrigatório")]
        public string GrantType { get; set; }
    }
}
