using System.Collections.Generic;
using System.Linq;
using meow.core.Interfaces;
using meow.core.Models;

namespace meow.core.Logic
{
    /// <summary>
    /// Бизнес-логика для работы с котами.
    /// </summary>
    public class CatLogic
    {
        private readonly IRepository<Cat> repository;

        /// <summary>
        /// Создаёт логику с указанным репозиторием.
        /// </summary>
        /// <param name="repository">Репозиторий для работы с БД (EF Core или Dapper).</param>
        public CatLogic(IRepository<Cat> repository)
        {
            this.repository = repository;
        }
        /// <summary>
        /// Добавить нового кота.
        /// </summary>
        public void AddCat(string name, string breed, int age, double weight, string color)
        {
            var cat = new Cat { Name = name, Breed = breed, Age = age, Weight = weight, Color = color };
            repository.Create(cat);
        }
        /// <summary>
        /// Получить всех котов.
        /// </summary>
        public List<Cat> GetAllCats()
        {
            return repository.ReadAll().ToList();
        }
        /// <summary>
        /// Получить кота по Id.
        /// </summary>
        public Cat GetCatById(int id)
        {
            return repository.ReadById(id);
        }
        /// <summary>
        /// Обновить данные кота.
        /// </summary>
        public void UpdateCat(Cat cat)
        {
            repository.Update(cat);
        }
        /// <summary>
        /// Удалить кота.
        /// </summary>
        public void DeleteCat(Cat obj)
        {
            repository.Delete(obj);
        }
        /// <summary>
        /// Сгруппировать котов по породе.
        /// </summary>
        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return repository.ReadAll()
                .GroupBy(c => c.Breed)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
        /// <summary>
        /// Получить котов тяжелее заданного веса.
        /// </summary>
        public List<Cat> GetCatsHeavierThan(double minWeight)
        {
            return repository.ReadAll()
                .Where(c => c.Weight > minWeight)
                .OrderByDescending(c => c.Weight)
                .ToList();
        }
    }
}
