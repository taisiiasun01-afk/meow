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
 
        public Cat AddCat(string name, string breed, int age, double weight, string color)
        {
            var cat = new Cat(nextId++, name, breed, age, weight, color);
            cats.Add(cat);
            return cat;
        }


        public List<Cat> GetAllCats()
        {
            return cats;
        }

        public Cat GetCatById(int id)
        {
            return cats.FirstOrDefault(c => c.Id == id);
        }

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


        public bool DeleteCat(int id)
        {
            var cat = GetCatById(id);
            if (cat == null) return false;

            cats.Remove(cat);
            return true;
        }


        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return cats
                .GroupBy(c => c.Breed)
                .ToDictionary(g => g.Key, g => g.ToList());
        }


        public List<Cat> GetCatsHeavierThan(double minWeight)
        {
            return cats
                .Where(c => c.Weight > minWeight)
                .OrderByDescending(c => c.Weight)
                .ToList();
        }


        public double GetAverageAge()
        {
            if (cats.Count == 0) return 0;
            return cats.Average(c => c.Age);
        }
    }
}

