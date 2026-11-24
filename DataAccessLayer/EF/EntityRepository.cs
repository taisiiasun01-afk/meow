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
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject, new()
    {
        /// <summary>
        /// создать новую запись в бд
        /// </summary>
        /// <param name="obj">объект который нужно добавить</param>
        public void Create(T obj)
        {
            using (var context = new CatsContext())
            {
                context.Set<T>().Add(obj);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// получить все записи из таблицы
        /// </summary>
        /// <returns>коллекция объектов</returns>
        public IEnumerable<T> ReadAll()
        {
            using (var context = new CatsContext())
            {
                return context.Set<T>().ToList();
            }
        }
        /// <summary>
        /// получить все записи по айди
        /// </summary>
        /// <param name="id">номер</param>
        /// <returns>объект</returns>
        public T ReadById(int id)
        {
            using (var context = new CatsContext())
            {
                return context.Set<T>().Find(id);
            }
        }
        /// <summary>
        /// обновить запись в бд
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
        /// удалить запись из бд
        /// </summary>
        /// <param name="obj">объект который нужно удалить</param>
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
