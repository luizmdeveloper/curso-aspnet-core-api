using Dapper;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using LuizMario.Dto.Filter;
using LuizMario.Dto.Pagination;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LuizMario.Domain.Core.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(IDbConnection connection) : base(connection)
        {
        }

        public ResponsePaginationDto<Category> Search(CategoryFilterDto filter) 
        {
            var sql = new StringBuilder(@" SELECT * FROM categories ");
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" WHERE UPPER(name) LIKE UPPER(@name)");
                parameters.Add("name", "%" + filter.Name + "%");
            }

            sql.Append(" LIMIT @page, @size ");
            parameters.Add("page", filter.Page != 0 ? filter.Page * filter.Size : filter.Page);
            parameters.Add("size", filter.Size == 0 ? 5 : filter.Size);

            var categories = _connection.Query<Category>(sql.ToString(), parameters);
            return new ResponsePaginationDto<Category>(TotalElements(filter), categories);
        }

        private int TotalElements(CategoryFilterDto filter)
        {
            var sql = new StringBuilder(@" SELECT count(id) FROM categories ");
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" WHERE UPPER(name) LIKE UPPER(@name)");
                parameters.Add("name", "%" + filter.Name + "%");
            }

            return _connection.ExecuteScalar<int>(sql.ToString(), parameters);
        }
    }
}
