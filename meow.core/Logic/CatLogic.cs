using meow.core.interfaces;
using Shared;
using meow.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using meow.core.interfaces.Interfaces;

namespace meow.core.Logic
{
    /// <summary>
    /// Бизнес-логика для работы с котами.
    /// <item><see cref="ICatModel"/>  для работы через Presenter (паттерн MVP).</item>
    /// <item><see cref="ICatLogic"/>  для работы через DI-контейнер (лабораторная №3).</item>
    /// </list>
    /// </summary>
    public class CatLogic : ICatModel, ICatLogic

    {
        /// <summary>Сервис для доступа к данным.</summary>
        private readonly CatService catService;
        /// <summary>
        /// Событие, сообщающее об изменении данных в модели.
        /// На него подписывается Presenter, чтобы перерисовать View.
        /// </summary>
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
        /// Добавить нового кота в БД (используется Presenter-ом).
        /// После добавления уведомляет подписчиков через событие DataChanged.
        /// </summary>
        /// <param name="cat">Объект Cat с данными нового кота.</param>
        public void Insert(string name, string breed, int age, double weight, string color)
        {
            catService.AddCat(name, breed, age, weight, color);
            DataChanged?.Invoke();
        }
        /// <summary>
        /// Обновить данные существующего кота (используется Presenter-ом).
        /// После обновления уведомляет подписчиков через событие DataChanged.
        /// </summary>
        /// <param name="cat">Объект Cat с обновленными данными.</param>
        public void Update(int id, string name, string breed, int age, double weight, string color)
        {
            var cat = catService.GetCatById(id);
            if (cat != null)
            {
                cat.Name = name;
                cat.Breed = breed;
                cat.Age = age;
                cat.Weight = weight;
                cat.Color = color;
                catService.UpdateCat(cat);
                DataChanged?.Invoke();
            }
        }
        /// <summary>
        /// Удалить кота по его идентификатору (используется Presenter-ом).
        /// После удаления уведомляет подписчиков через событие DataChanged.
        /// </summary>
        /// <param name="id">Идентификатор кота, которого нужно удалить.</param>
        public void Delete(int id)
        {
            var cat = catService.GetCatById(id);
            if (cat != null)
            {
                catService.DeleteCat(cat);
                DataChanged?.Invoke();
            }
        }
        /// <summary>
        /// Добавить нового кота (реализация ICatLogic).
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
        //// <summary>
        /// Получить кота по идентификатору (реализация ICatLogic).
        /// </summary>
        /// <param name="id">Идентификатор кота.</param>
        /// <returns>Объект Cat или null, если кот не найден.</returns>
        public object GetCatById(int id)
        {
            return catService.GetCatById(id);
        }
        /// <summary>
        /// Обновить существующего кота (реализация ICatLogic).
        /// </summary>
        /// <param name="cat">Объект Cat с обновленными данными.</param>
        public void UpdateCat(object catObj)
        {
            if (catObj is Cat cat)
            {
                catService.UpdateCat(cat);
            }
        }
        /// <summary>
        /// Удалить кота из БД (реализация ICatLogic).
        /// </summary>
        /// <param name="obj">Объект Cat, который нужно удалить.</param>
        public void DeleteCat(object obj)
        {
            if (obj is Cat cat)
            {
                catService.DeleteCat(cat);
            }
        }
        /// <summary>
        /// Получить список всех котов (реализация ICatLogic).
        /// </summary>
        /// <returns>Список объектов Cat.</returns>
        public List<object> GetAllCats()
        {
            return catService.GetAllCats().Cast<object>().ToList();
        }
        /// <summary>
        /// Получить список всех котов (реализация ICatLogic).
        /// </summary>
        /// <returns>Список объектов Cat.</returns>
        public List<object> GetAll()
        {
            return catService.GetAllCats().Cast<object>().ToList();
        }        /// <summary>
                 /// Сгруппировать всех котов по породе.
                 /// </summary>
                 /// <returns>Словарь: ключ — название породы, значение — список котов этой породы.</returns>
        public Dictionary<string, List<object>> GroupByBreed()
        {
            return catService.GetAllCats()
                            .GroupBy(c => c.Breed)
                            .ToDictionary(g => g.Key, g => g.Cast<object>().ToList());
        }
        /// <summary>
        /// Получить котов, вес которых больше указанного значения.
        /// </summary>
        /// <param name="minWeight">Минимальный вес в килограммах.</param>
        /// <returns>Список котов, отсортированный по убыванию веса.</returns>
        public List<object> GetCatsHeavierThan(double minWeight)
        {
            return catService.GetAllCats()
                            .Where(c => c.Weight > minWeight)
                            .OrderByDescending(c => c.Weight)
                            .Cast<object>()
                            .ToList();
        }
    }
}
