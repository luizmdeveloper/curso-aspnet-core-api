using Dapper;
using Domain.Infraestructure.Notifications;
using Domain.Infraestructure.Repository;
using LuizMario.Domain.Core.Entity;
using LuizMario.Dto.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LuizMario.Domain.Core.Repository
{
    public class FinancialMovimentRepository : Repository<FinancialMoviment>
    {
        public FinancialMovimentRepository(IDbConnection connection, INotification notification) : base(connection, notification)
        {
        }

        public int CalculeteTotalElements(FinancialMovimentFilterDto filter)
        {
            var sql = new StringBuilder(@" SELECT COUNT(id) FROM financialmoviments
                                           WHERE financialmoviments.dateCreated BETWEEN @from AND @to ");
            var parameters = new Dictionary<string, object>();
            parameters.Add("from", filter.from);
            parameters.Add("to", filter.to);

            if (filter.Category != 0) 
            {
                sql.Append($" AND categoryId = @category");
                parameters.Add("category", filter.Category);
            }

            if (filter.Person != 0)
            {
                sql.Append($"AND personId = @person");
                parameters.Add("person", filter.Person);
            }

            return _connection.ExecuteScalar<int>(sql.ToString(), parameters);
        }

        public IEnumerable<FinancialMoviment> Find(FinancialMovimentFilterDto filter)
        {
            var sql = new StringBuilder(@" SELECT * FROM financialmoviments
                                           INNER JOIN categories ON categories.id = financialmoviments.categoryId
                                           INNER JOIN persons ON persons.id = financialmoviments.personId
                                           WHERE financialmoviments.dateCreated BETWEEN @from AND @to ");
            var parameters = new Dictionary<string, object>();
            parameters.Add("from", filter.from);
            parameters.Add("to", filter.to);

            if (filter.Category != 0)
            {
                sql.Append($" AND categoryId = @category");
                parameters.Add("category", filter.Category);
            }

            if (filter.Person != 0)
            {
                sql.Append($"AND personId = @person");
                parameters.Add("person", filter.Person);
            }

            return _connection.Query<FinancialMoviment, Category, Person, FinancialMoviment>(
                sql.ToString(), 
                (moviment, cateogry, person) => 
                {
                    moviment.Category = cateogry;
                    moviment.Person = person;
                    return moviment;
                }, 
                splitOn: "id",
                param: parameters);
        }
    }
}
