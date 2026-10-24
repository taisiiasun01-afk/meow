using System.Collections.Generic;
using System.Linq;
using meow.core.Models;
using meow.core.interfaces;

namespace meow.core.Logic
{
    /// <summary>
    /// Бизнес-логика для работы с котами.
    /// </summary>
    public class CatLogic
    {
        private IRepository<Cat> repository;

        /// <summary>
        /// Создаёт логику с указанным репозиторием.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с БД (EF Core или Dapper).</param>
        public CatLogic(IRepository<Cat> repository)
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
        /// <summary>
        /// Сгруппировать котов по породе
        /// </summary>
        /// <returns>список котов с одной породой</returns>
        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return repository.ReadAll()
                .GroupBy(c => c.Breed)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
        /// <summary>
        /// Получить котов тяжелее заданного веса
        /// </summary>
        /// <param name="minWeight">вес, для сравнения</param>
        /// <returns>список котов, тяжелее заданного веса</returns>
        public List<Cat> GetCatsHeavierThan(double minWeight)
        {
            return repository.ReadAll()
                .Where(c => c.Weight > minWeight)
                .OrderByDescending(c => c.Weight)
                .ToList();
        }
    }
}
