using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared
{
    /// <summary>
    /// Интерфейс модели. Презентер работает с ней через этот интерфейс.
    /// </summary>
    public interface ICatModel
    {
        event Action DataChanged;
        /// <summary>
        /// Добавить нового кота в базу данных
        /// </summary>
        /// <param name="name">Кличка</param>
        /// <param name="breed">Парода</param>
        /// <param name="age">Возраст</param>
        /// <param name="weight">Вес</param>
        /// <param name="color">Расцветка</param>
        void Insert(string name, string breed, int age, double weight, string color);
        /// <summary>
        /// Обновить данные существующего кота
        /// </summary>
        /// <param name="name">Кличка</param>
        /// <param name="breed">Парода</param>
        /// <param name="age">Возраст</param>
        /// <param name="weight">Вес</param>
        /// <param name="color">Расцветка</param>
        void Update(int id, string name, string breed, int age, double weight, string color);
        /// <summary>
        /// Удалить кота из бд
        /// </summary>
        /// <param name="id">номер</param>
        void Delete(int id);
        /// <summary>
        /// Получать список всех котов
        /// </summary>
        /// <returns>список объектов</returns>
        List<object> GetAll();
    }
}
