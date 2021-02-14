using System;
using System.ComponentModel.DataAnnotations;

namespace LuizMario.Dto.Filter
{
    public class FinancialMovimentFilterDto : BaseFilterDto
    {
        public int Category { get; set; }
        public int Person { get; set; }
        [Required]
        public DateTime from { get; set; }
        [Required]
        public DateTime to { get; set; }
    }
}
