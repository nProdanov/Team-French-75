using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Data.Repositories
{
    public class EfGenericRepostitory<T> : IRepository<T> where T: class
    {
        public EfGenericRepostitory(ITravelAgencyDbContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public ITravelAgencyDbContext Context { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public void Add(T entity)
        {
            this.DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            this.DbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet.OfType<T>().ToList();
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        //public void Update(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //private DbEntityEntry AttachIfDetached(T entity)
        //{
        //    var entry = this.Context.Entry(entity);
        //    if (entry.State == EntityState.Detached)
        //    {
        //        this.DbSet.Attach(entity);
        //    }

        //    return entry;
        //}
    }
}
