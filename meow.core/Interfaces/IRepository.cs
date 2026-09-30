using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meow.core.Interfaces
{
    /// <summary>
    /// Базовый интерфейс репозитория.
    /// Определяет CRUD-операции для любой сущности.
    /// </summary>
    /// <typeparam name="T">Тип сущности (например, Cat).</typeparam>
    public interface IRepository<T> where T : IDomainObject
    {
        /// <summary>Добавить новую сущность в БД.</summary>
        void Add(T entity);

        /// <summary>Удалить сущность по Id.</summary>
        void Delete(int id);

        /// <summary>Получить все сущности.</summary>
        IEnumerable<T> ReadAll();

        /// <summary>Получить сущность по Id.</summary>
        T ReadById(int id);

        /// <summary>Обновить сущность.</summary>
        void Update(T entity);
    }
}
