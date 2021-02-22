using System.ComponentModel.DataAnnotations;

namespace LuizMario.Dto.Input
{
    public class UserInputDto
    {
        [Required(ErrorMessage = "Email é obrigatória")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }

        [Required(ErrorMessage = "GrantType é obrigatório")]
        public string GrantType { get; set; }
    }
}
