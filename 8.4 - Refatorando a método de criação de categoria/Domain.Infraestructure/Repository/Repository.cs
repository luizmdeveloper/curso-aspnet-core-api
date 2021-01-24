using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;

namespace Domain.Infraestructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbConnection _connection;

        public Repository(IDbConnection connection) 
        {
            this._connection = connection;
            this._connection.ConnectionString = "Server=localhost;Port=3306;Database=finaceiro;Uid=root;Pwd=root;persistsecurityinfo=True;";
            this._connection.Open();
        }

        public void Delete(T entity)
        {
            _connection.Delete(entity);
        }

        public IEnumerable<T> FindAll()
        {
            return _connection.GetAll<T>();
        }

        public T FindById(int Id)
        {
            return _connection.Get<T>(Id);
        }

        public void Save(T entity)
        {
            _connection.Insert<T>(entity);
        }

        public void Update(T entity)
        {
            _connection.Update<T>(entity);
        }
    }
}
