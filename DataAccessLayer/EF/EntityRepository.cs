using meow.core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF
{
    /// <summary>
    /// Репозиторий на основе Entity Framework.
    /// </summary>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        //private readonly DbContextOptions<CatsContext> options;

        //public EntityRepository(DbContextOptions<CatsContext> options)
        //{
        //    this.options = options;
        //}

        public void Create(T obj)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Add(obj);
                context.SaveChanges();
            }
        }

        public IEnumerable<T> ReadAll()
        {
            using (var context = new CatsContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public T ReadById(int id)
        {
            using (var context = new CatsContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        public void Update(T obj)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Update(obj);
                context.SaveChanges();
            }
        }

        public void Delete(T obj)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Remove(obj);
                context.SaveChanges();
            }
        }
    }
}
