using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuizMario.Domain.Core.Entity
{
    [Table("authorities")]
    public class Authorization
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Role é obrigatória")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descrition { get; set; }
    }
}
