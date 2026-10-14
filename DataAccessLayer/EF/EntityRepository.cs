using meow.core.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAccessLayer.EF
{
    /// <summary>
    /// Репозиторий на основе Entity Framework.
    /// </summary>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        /// <summary>
        /// Создать новую запись
        /// </summary>
        /// <param name="obj">Объект, который нужно добавить</param>
        public void Create(T obj)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Add(obj);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>коллекция всех типов</returns>
        public IEnumerable<T> ReadAll()
        {
            using (var context = new CatsContext())
            {
                return context.Set<T>().ToList();
            }
        }
        /// <summary>
        /// Получить запись по Id
        /// </summary>
        /// <param name="id">номер</param>
        /// <returns>объект по номеру</returns>
        public T ReadById(int id)
        {
            using (var context = new CatsContext())
            {
                return context.Set<T>().Find(id);
            }
        }
        /// <summary>
        /// Обновить существующую запись
        /// </summary>
        /// <param name="obj">объект с обновленными данными</param>
        public void Update(T obj)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Update(obj);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="obj">объект для удаления из бд</param>
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
