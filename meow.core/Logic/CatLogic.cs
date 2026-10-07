using System.Collections.Generic;
using System.Linq;
using meow.core.Interfaces;
using meow.core.Models;

namespace meow.core.Logic
{
    public class CatLogic
    {
        private readonly IRepository<Cat> repository;

        public CatLogic(IRepository<Cat> repository)
        {
            this.repository = repository;
        }

        public void AddCat(string name, string breed, int age, double weight, string color)
        {
            var cat = new Cat { Name = name, Breed = breed, Age = age, Weight = weight, Color = color };
            repository.Add(cat);
        }

        public IEnumerable<Cat> GetAllCats()
        {
            return repository.ReadAll();
        }

        public Cat GetCatById(int id)
        {
            return repository.ReadById(id);
        }

        public void UpdateCat(int id, string newName, string newBreed,
                              int newAge, double newWeight, string newColor)
        {
            var cat = repository.ReadById(id);
            if (cat == null) return;

            cat.Name = newName;
            cat.Breed = newBreed;
            cat.Age = newAge;
            cat.Weight = newWeight;
            cat.Color = newColor;
            repository.Update(cat);
        }

        public void DeleteCat(int id)
        {
            repository.Delete(id);
        }

        public Dictionary<string, List<Cat>> GroupByBreed()
        {
            return repository.ReadAll()
                .GroupBy(c => c.Breed)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<Cat> GetCatsHeavierThan(double minWeight)
        {
            return repository.ReadAll()
                .Where(c => c.Weight > minWeight)
                .OrderByDescending(c => c.Weight)
                .ToList();
        }
    }
}
