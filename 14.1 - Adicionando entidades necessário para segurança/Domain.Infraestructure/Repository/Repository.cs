using Dapper.Contrib.Extensions;
using Domain.Infraestructure.Notifications;
using System.Collections.Generic;
using System.Data;

namespace Domain.Infraestructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbConnection _connection;
        private readonly INotification _notification;

        public Repository(IDbConnection connection, INotification notification) 
        {
            this._connection = connection;
            this._connection.Close();
            this._connection.ConnectionString = "Server=localhost;Port=3306;Database=finaceiro;Uid=root;Pwd=root;persistsecurityinfo=True;";
            this._connection.Open();
            this._notification = notification;
        }

        public virtual void Delete(T entity)
        {
            _connection.Delete(entity);
        }

        public virtual IEnumerable<T> FindAll()
        {
            return _connection.GetAll<T>();
        }

        public virtual T FindById(int Id)
        {
            return _connection.Get<T>(Id);
        }

        public virtual void Save(T entity)
        {
            _connection.Insert<T>(entity);
        }

        public virtual void Update(T entity)
        {
            _connection.Update<T>(entity);
        }
    }
}
