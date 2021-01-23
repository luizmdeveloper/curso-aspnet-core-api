using System.Collections.Generic;

namespace Domain.Infraestructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> FindAll();
        T FindById(int Id);
        void Save(T entity);
        void Update(T entity);
        void Delte(T entity);
    }
}
