using Dapper;
using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using LuizMario.Dto.Filter;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LuizMario.Domain.Core.Repository
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(IDbConnection connection, INotification notification) : base(connection, notification)
        {
        }

        public int CalculeteTotalElements(PersonFilterDto filter)
        {
            var sql = new StringBuilder(" SELECT COUNT(id) FROM persons ");
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(filter.Name)) 
            {
                sql.Append(" WHERE UPPER(name) LIKE UPPER(@name)");
                parameters.Add("name", "%" + filter.Name + "%");
            }

            return _connection.ExecuteScalar<int>(sql.ToString(), parameters);
        }

        public IEnumerable<Person> Find(PersonFilterDto filter)
        {
            var sql = new StringBuilder(" SELECT * FROM persons ");
            var parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(filter.Name))
            {
                sql.Append(" WHERE UPPER(name) LIKE UPPER(@name)");
                parameters.Add("name", "%" + filter.Name + "%");
            }

            sql.Append(" LIMIT @page, @size ");
            parameters.Add("page", filter.Page != 0 ? filter.Page * filter.Size : filter.Page);
            parameters.Add("size", filter.Size == 0 ? 5 : filter.Size);

            return _connection.Query<Person>(sql.ToString(), parameters);
        }
    }
}
