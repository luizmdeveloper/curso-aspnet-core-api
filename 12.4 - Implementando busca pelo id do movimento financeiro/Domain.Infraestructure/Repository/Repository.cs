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
            this._connection.ConnectionString = "Server=localhost;Port=3306;Database=finaceiro;Uid=root;Pwd=root;persistsecurityinfo=True;";
            this._connection.Open();
            this._notification = notification;
        }

        public virtual void Delete(T entity)
        {
            try
            {
                _connection.Delete(entity);
            }
            catch (System.Exception ex)
            {
                AddErrorFatal(ex.Message);
            }
        }

        public virtual IEnumerable<T> FindAll()
        {
            try
            {
                return _connection.GetAll<T>();
            }
            catch (System.Exception ex)
            {
                AddErrorFatal(ex.Message);
                return null;
            }
        }

        public virtual T FindById(int Id)
        {
            try
            {
                return _connection.Get<T>(Id);
            }
            catch (System.Exception ex)
            {
                AddErrorFatal(ex.Message);
                return null;
            }
        }

        public virtual void Save(T entity)
        {
            try
            {
                _connection.Insert<T>(entity);
            }
            catch (System.Exception ex)
            {
                AddErrorFatal(ex.Message);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                _connection.Update<T>(entity);
            }
            catch (System.Exception ex)
            {
                AddErrorFatal(ex.Message);
            }
        }

        private void AddErrorFatal(string message)
        {
            _notification.AddErrorFatal(message);
        }
    }
}
