using meow.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meow.core.interfaces.Interfaces
{
    /// <summary>
    /// Интерфейс бизнес-логики для работы с котами.
    /// </summary>
    public interface ICatLogic
    {
        /// <summary>
        /// Добавить нового кота в базу данных.
        /// </summary>
        /// <param name="name">Кличка кота.</param>
        /// <param name="breed">Порода кота.</param>
        /// <param name="age">Возраст в годах.</param>
        /// <param name="weight">Вес в килограммах.</param>
        /// <param name="color">Окрас кота.</param>
        void AddCat(string name, string breed, int age, double weight, string color);
        /// <summary>
        /// Получить список всех котов.
        /// </summary>
        /// <returns>Список объектов Cat. Если котов нет — пустой список.</returns>
        List<Cat> GetAllCats();
        /// <summary>
        /// Получить кота по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор кота.</param>
        /// <returns>Объект Cat или null, если кот не найден.</returns>
        Cat GetCatById(int id);
        /// <summary>
        /// Обновить данные существующего кота.
        /// </summary>
        /// <param name="cat">Объект Cat с обновлёнными данными.</param>
        void UpdateCat(Cat cat);
        /// <summary>
        /// Удалить кота из базы данных.
        /// </summary>
        /// <param name="obj">Объект Cat, который нужно удалить.</param>
        void DeleteCat(Cat obj);
        /// <summary>
        /// Сгруппировать всех котов по породе.
        /// </summary>
        /// <returns>Словарь, где ключ — название породы, значение — список котов этой породы.</returns>

        Dictionary<string, List<Cat>> GroupByBreed();
        /// <summary>
        /// Получить котов, вес которых больше указанного значения.
        /// </summary>
        /// <param name="minWeight">Минимальный вес в килограммах.</param>
        /// <returns>Список котов, отсортированный по убыванию веса.</returns>
        List<Cat> GetCatsHeavierThan(double minWeight);
    }
}