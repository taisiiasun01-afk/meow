
using meow.core.Models;
using Shared;
using System;
using System.Collections.Generic;

namespace meow.core.Interfaces
{
    public interface ICatLogic
    {
        /// <summary>
        /// Событие, возникающее при изменении данных
        /// </summary>
        event EventHandler DataChanged;
        /// <summary>
        /// добавить кота
        /// </summary>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        void AddCat(
            string name,
            string breed,
            int age,
            double weight,
            string color);
        /// <summary>
        /// обновление данных кота
        /// </summary>
        /// <param name="name">кличка</param>
        /// <param name="breed">порода</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <param name="color">окрас</param>
        /// <returns>true, если кот найден и обновлен; false, если кот не найден</returns>
        bool UpdateCat(
            int id,
            string name,
            string breed,
            int age,
            double weight,
            string color);
        /// <summary>
        /// удаление кота из бд
        /// </summary>
        /// <param name="id">айди</param>
        /// <returns>true, если кот найден и удален; false, если кот не найден</returns>
        bool DeleteCat(int id);
        /// <summary>
        /// Получить список всех котов в виде DTO
        /// </summary>
        /// <returns>список</returns>
        IReadOnlyList<CatDto> GetAllCats();
        /// <summary>
        /// Сгруппировать всех котов по породе
        /// </summary>
        /// <returns>список</returns>
        IReadOnlyList<BreedGroupDto> GroupByBreed();
        /// <summary>
        /// Получить котов, вес которых больше указанного значения
        /// </summary>
        /// <param name="minWeight">минимальный вес</param>
        /// <returns>список отсортерованный по убыванию</returns>
        IReadOnlyList<CatDto> GetCatsHeavierThan(double minWeight);
    }
}
