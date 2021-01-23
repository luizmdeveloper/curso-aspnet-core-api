using System.Collections.Generic;

namespace finaceiro_api.Pagination
{
    public class ResponsePagination<T> where T : class
    {
        public ResponsePagination(int TotalElements, IEnumerable<T> Content) 
        {
            this.TotalELements = TotalElements;
            this.Content = Content;
        }

        public int TotalELements { get; }
        public IEnumerable<T> Content { get; }
    }
}
