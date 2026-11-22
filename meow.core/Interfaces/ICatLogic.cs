
using meow.core.Models;
using Shared;
using System;
using System.Collections.Generic;

namespace meow.core.Interfaces
{
    public interface ICatLogic
    {
        event EventHandler DataChanged;

        void AddCat(
            string name,
            string breed,
            int age,
            double weight,
            string color);

        bool UpdateCat(
            int id,
            string name,
            string breed,
            int age,
            double weight,
            string color);

        bool DeleteCat(int id);

        IReadOnlyList<CatDto> GetAllCats();

        IReadOnlyList<BreedGroupDto> GroupByBreed();

        IReadOnlyList<CatDto> GetCatsHeavierThan(double minWeight);
    }
}
