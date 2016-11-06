using System.Collections.Generic;

namespace TravelAgency.Data.Repositories
{
   public interface IRepository<T> where T : class
    {
        T GetById(object id);

        IEnumerable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);
    }
}
