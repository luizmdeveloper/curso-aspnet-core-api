using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuizMario.Domain.Core.Entity
{
    [Table("financialmoviments")]
    public class FinancialMoviment
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Categoria é obrigatória.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Pessoa é obrigatória.")]
        public int PersonId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DatePaid { get; set; }
        public string Observation { get; set; }

        [Required(ErrorMessage = "Tipo operação é obrigatório.")]
        public string Type { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Category Category { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public Person Peson { get; set; }
    }
}
