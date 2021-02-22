using System.ComponentModel.DataAnnotations;

namespace LuizMario.Domain.Core.Entity
{
    public class User
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatória")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatória")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; }

        public int ProfileId { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Profile Profile { get; set; }
    }
}
