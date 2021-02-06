using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuizMario.Domain.Core.Entity
{
    [Table("persons")]
    public class Person
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
    }
}
