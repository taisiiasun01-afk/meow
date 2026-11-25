
using meow.core.Interfaces;
using meow.core.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace meow.core.Logic
{
    public class CatLogic : ICatLogic
    {
        /// <summary>
        /// сервис для доступа к данным
        /// </summary>
        private readonly CatService catService;
        /// <summary>
        /// Событие, сообщающее об изменении данных
        /// </summary>
        public event EventHandler DataChanged;

        public CatLogic(CatService catService)
        {
            this.catService = catService ?? throw new ArgumentNullException(nameof(catService));
        }
        /// <summary>
        /// добавить нового кота
        /// </summary>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        public void AddCat(
            string name,
            string breed,
            int age,
            double weight,
            string color)
        {
            catService.AddCat(name, breed, age, weight, color);
            OnDataChanged();
        }
        /// <summary>
        /// обновить данные о коте
        /// </summary>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        /// <returns>true, если кот найден и обновлен; false, если кот не найден</returns>
        public bool UpdateCat(int id, string name, string breed, int age, double weight, string color)
        {
            Cat cat = catService.GetCatById(id);

            if (cat == null)
                return false;

            cat.Name = name;
            cat.Breed = breed;
            cat.Age = age;
            cat.Weight = weight;
            cat.Color = color;

            catService.UpdateCat(cat);
            OnDataChanged();

            return true;
        }
        /// <summary>
        /// удалить кота из бд
        /// </summary>
        /// <param name="id">номер</param>
        /// <returns>true, если кот найден и удален; false, если кот не найден</returns>
        public bool DeleteCat(int id)
        {
            Cat cat = catService.GetCatById(id);

            if (cat == null)
                return false;

            catService.DeleteCat(cat);
            OnDataChanged();

            return true;
        }
        /// <summary>
        /// Получить список всех котов в виде DTO
        /// </summary>
        /// <returns>список</returns>
        public IReadOnlyList<CatDto> GetAllCats()
        {
            return catService.GetAllCats()
                .Select(ToDto)
                .ToList();
        }
        /// <summary>
        /// Сгруппировать всех котов по породе
        /// </summary>
        /// <returns>список</returns>
        public IReadOnlyList<BreedGroupDto> GroupByBreed()
        {
            return catService.GetAllCats()
                .GroupBy(cat => cat.Breed)
                .Select(group => new BreedGroupDto
                {
                    Breed = group.Key,
                    Cats = group.Select(ToDto).ToList()
                })
                .OrderBy(group => group.Breed)
                .ToList();
        }
        /// <summary>
        /// Получить котов, вес которых больше указанного значения, в виде DTO
        /// </summary>
        /// <param name="minWeight">минимальный вес</param>
        /// <returns>список</returns>
        public IReadOnlyList<CatDto> GetCatsHeavierThan(
            double minWeight)
        {
            return catService.GetAllCats()
                .Where(cat => cat.Weight > minWeight)
                .OrderByDescending(cat => cat.Weight)
                .Select(ToDto)
                .ToList();
        }
        /// <summary>
        /// Преобразует доменную модель в DTO
        /// </summary>
        /// <param name="cat">Объект доменной модели</param>
        /// <returns>Объект DTO</returns>
        private static CatDto ToDto(Cat cat)
        {
            return new CatDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Breed = cat.Breed,
                Age = cat.Age,
                Weight = cat.Weight,
                Color = cat.Color
            };
        }
        /// <summary>
        /// Вызывает событие, уведомляя подписчиков об изменении данных.
        /// </summary>
        private void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
