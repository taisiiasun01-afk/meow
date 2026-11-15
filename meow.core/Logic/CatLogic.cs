using meow.core.interfaces;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using meow.core.interfaces.Interfaces;

namespace meow.core.Logic
{
    /// <summary>
    /// Бизнес-логика для работы с котами.
    /// Содержит только правила обработки данных, не зависит от источника данных.
    /// </summary>
    public class CatLogic : ICatModel, ICatLogic

    {
        private readonly CatService catService;
        public event Action DataChanged;
        /// <summary>
        /// Создает экземпляр бизнес-логики.
        /// </summary>
        /// <param name="catService">Сервис для доступа к данным.</param>
        public CatLogic(CatService catService)
        {
            this.catService = catService;
        }
        /// <summary>
        /// Добавить нового кота.
        /// </summary>
        /// <param name="name">Кличка кота.</param>
        /// <param name="breed">Порода кота.</param>
        /// <param name="age">Возраст в годах.</param>
        /// <param name="weight">Вес в килограммах.</param>
        /// <param name="color">Окрас кота.</param>
        public void Insert(Cat cat)
        {
            catService.AddCat(cat.Name, cat.Breed, cat.Age, cat.Weight, cat.Color);
            DataChanged?.Invoke(); // Сообщаем, что данные изменились
        }

        public void Update(Cat cat)
        {
            catService.UpdateCat(cat);
            DataChanged?.Invoke();
        }

        public void Delete(int id)
        {
            var cat = catService.GetCatById(id);
            if (cat != null)
            {
                catService.DeleteCat(cat);
                DataChanged?.Invoke();
            }
        }

        public void AddCat(string name, string breed, int age, double weight, string color)
        {
            catService.AddCat(name, breed, age, weight, color);
        }

        public List<Cat> GetAllCats()
        {
            return catService.GetAllCats();
        }

        public Cat GetCatById(int id)
        {
            return catService.GetCatById(id);
        }

        public void UpdateCat(Cat cat)
        {
            catService.UpdateCat(cat);
        }

        public void DeleteCat(Cat obj)
        {
            catService.DeleteCat(obj);
        }
        public List<Cat> GetAll() => catService.GetAllCats();

        // Твои бизнес-функции (если нужны презентеру)
        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return GetAll().GroupBy(c => c.Breed).ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<Cat> GetCatsHeavierThan(double minWeight)
        {
            return GetAll().Where(c => c.Weight > minWeight).OrderByDescending(c => c.Weight).ToList();
        }
    }
}
