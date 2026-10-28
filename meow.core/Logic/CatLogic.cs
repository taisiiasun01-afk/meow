using meow.core.interfaces;
using meow.core.interfaces.Interfaces;
using meow.core.Models;
using System.Collections.Generic;
using System.Linq;

namespace meow.core.Logic
{
    /// <summary>
    /// Бизнес-логика для работы с котами.
    /// Содержит только правила обработки данных, не зависит от источника данных.
    /// </summary>
    public class CatLogic: ICatLogic

    {
        private readonly CatService catService;
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
        public void AddCat(string name, string breed, int age, double weight, string color)
        {
            catService.AddCat(name, breed, age, weight, color);
        }
        /// <summary>
        /// Получить всех котов из базы данных.
        /// </summary>
        /// <returns>Список всех котов.</returns>
        public List<Cat> GetAllCats()
        {
            return catService.GetAllCats();
        }
        /// <summary>
        /// Обновить данные существующего кота.
        /// </summary>
        /// <param name="cat">Объект Cat с обновленными данными.</param>
        public void UpdateCat(Cat cat)
        {
            catService.UpdateCat(cat);
        }
        /// <summary>
        /// Удалить кота из базы данных.
        /// </summary>
        /// <param name="obj">Объект Cat, который нужно удалить.</param>
        public void DeleteCat(Cat obj)
        {
            catService.DeleteCat(obj);
        }
        /// <summary>
        /// Найти кота по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор кота.</param>
        /// <returns>Объект Cat или null, если кот не найден.</returns>
        public Cat GetCatById(int id)
        {
            return catService.GetCatById(id);
        }

        /// <summary>
        /// Сгруппировать котов по породе
        /// </summary>
        /// <returns>список котов с одной породой</returns>
        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return catService.GetAllCats()
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
            return catService.GetAllCats()
                .Where(c => c.Weight > minWeight)
                .OrderByDescending(c => c.Weight)
                .ToList();
        }
    }
}
