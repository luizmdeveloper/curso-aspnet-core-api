using System;

namespace LuizMario.Dto.Filter
{
    public class FinancialMovimentFilterDto : BaseFilterDto
    {
        public int Category { get; set; }
        public int Person { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
