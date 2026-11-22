
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
        private readonly CatService catService;

        public event EventHandler DataChanged;

        public CatLogic(CatService catService)
        {
            this.catService = catService ??
                throw new ArgumentNullException(nameof(catService));
        }

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

        public bool UpdateCat(
            int id,
            string name,
            string breed,
            int age,
            double weight,
            string color)
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

        public bool DeleteCat(int id)
        {
            Cat cat = catService.GetCatById(id);

            if (cat == null)
                return false;

            catService.DeleteCat(cat);
            OnDataChanged();

            return true;
        }

        public IReadOnlyList<CatDto> GetAllCats()
        {
            return catService.GetAllCats()
                .Select(ToDto)
                .ToList();
        }

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

        public IReadOnlyList<CatDto> GetCatsHeavierThan(
            double minWeight)
        {
            return catService.GetAllCats()
                .Where(cat => cat.Weight > minWeight)
                .OrderByDescending(cat => cat.Weight)
                .Select(ToDto)
                .ToList();
        }

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

        private void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
