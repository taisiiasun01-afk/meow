using System;
using System.Collections.Generic;
using System.Linq;
using meow.core.Models;
namespace meow.core.Logic
{
    public class CatLogic
    {
        private List<Cat> cats = new List<Cat>();
        private int nextId = 1;
        /// <summary>
        /// создание кота
        /// </summary>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        /// <returns>код ошибки</returns>
        public Cat AddCat(string name, string breed, int age, double weight, string color)
        {
            var cat = new Cat(nextId++, name, breed, age, weight, color);
            cats.Add(cat);
            return cat;
        }

        /// <summary>
        /// получить всех котов
        /// </summary>
        /// <returns></returns>
        public List<Cat> GetAllCats()
        {
            return cats;
        }
        /// <summary>
        /// получить одного кота
        /// </summary>
        /// <param name="id">номер</param>
        /// <returns></returns>
        public Cat GetCatById(int id)
        {
            return cats.FirstOrDefault(c => c.Id == id);
        }
        /// <summary>
        /// Изменение 
        /// </summary>
        /// <param name="id">номер</param>
        /// <param name="newName">новое имя</param>
        /// <param name="newBreed">новая порода</param>
        /// <param name="newAge">новый возраст</param>
        /// <param name="newWeight">новый вес</param>
        /// <param name="newColor">новый окрас</param>
        /// <returns></returns>
        public bool UpdateCat(int id, string newName, string newBreed, int newAge, double newWeight, string newColor)
        {
            var cat = GetCatById(id);
            if (cat == null) return false;

            cat.Name = newName;
            cat.Breed = newBreed;
            cat.Age = newAge;
            cat.Weight = newWeight;
            cat.Color = newColor;
            return true;
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="id">номер</param>
        /// <returns>ьтимюб.ьтимьюбюи</returns>
        public bool DeleteCat(int id)
        {
            var cat = GetCatById(id);
            if (cat == null) return false;

            cats.Remove(cat);
            return true;
        }

        /// <summary>
        /// группировка котов по породе
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return cats
                .GroupBy(c => c.Breed)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// поиск котов тяжелее указанного веса
        /// </summary>
        /// <param name="minWeight"></param>
        /// <returns></returns>
        public List<Cat> GetCatsHeavierThan(double minWeight)
        {
            return cats
                .Where(c => c.Weight > minWeight)
                .OrderByDescending(c => c.Weight)
                .ToList();
        }

    }
}

