using System.ComponentModel.DataAnnotations.Schema;

namespace finaceiro_api.Entity
{
    [Table("categories")]
    public class Category
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
