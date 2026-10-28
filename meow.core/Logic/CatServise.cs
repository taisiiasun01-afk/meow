using meow.core.interfaces;
using meow.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meow.core.Logic
{
    /// <summary>
    /// Сервис доступа к данным.
    /// Отвечает исключительно за выполнение CRUD-операций через репозиторий.
    /// </summary>
    public class CatService
    {
        private readonly IRepository<Cat> repository;

        public CatService(IRepository<Cat> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Добавить нового кота
        /// </summary>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        public void AddCat(string name, string breed, int age, double weight, string color)
        {
            var cat = new Cat { Name = name, Breed = breed, Age = age, Weight = weight, Color = color };
            repository.Create(cat);
        }
        /// <summary>
        /// Получить всех котов
        /// </summary>
        /// <returns>список всех котов</returns>
        public List<Cat> GetAllCats()
        {
            return repository.ReadAll().ToList();
        }

        /// <summary>
        /// Получить кота по Id
        /// </summary>
        /// <param name="id">номер</param>
        /// <returns>конкретного кота</returns>
        public Cat GetCatById(int id)
        {
            return repository.ReadById(id);
        }

        /// <summary>
        /// Обновить данные кота
        /// </summary>
        /// <param name="cat">объект с одновленными данными</param>

        public void UpdateCat(Cat cat)
        {
            repository.Update(cat);
        }

        /// <summary>
        /// Удалить кота
        /// </summary>
        /// <param name="obj">объект для удаления</param>
        public void DeleteCat(Cat obj)
        {
            repository.Delete(obj);
        }
    }
}
