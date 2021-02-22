using System.ComponentModel.DataAnnotations;

namespace LuizMario.Dto.Input
{
    public class LogoutInputDto
    {
        [Required(ErrorMessage = "RefreshToken é obrigatório")]
        public string RefreshToken { get; set; }
    }
}
