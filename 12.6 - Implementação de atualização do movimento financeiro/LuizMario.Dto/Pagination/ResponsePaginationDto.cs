using System.Collections.Generic;

namespace LuizMario.Dto.Pagination
{
    public class ResponsePaginationDto<T> where T : class
    {
        public ResponsePaginationDto(int TotalElements, IEnumerable<T> Content) 
        {
            this.TotalELements = TotalElements;
            this.Content = Content;
        }

        public int TotalELements { get; }
        public IEnumerable<T> Content { get; }
    }
}
