using meow.core.Interfaces;
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
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        public void Add(T entity)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new CatsContext())
            {
                var entity = context.Set<T>().Find(id);
                if (entity != null)
                {
                    context.Set<T>().Remove(entity);
                    context.SaveChanges();
                }
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

        public void Update(T entity)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
            }
        }
    }
}
