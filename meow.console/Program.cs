using meow.core.Logic;
using System;

namespace meow.console
{
    class Program
    {
        static CatLogic catLogic = new CatLogic();
        /// <summary>
        /// Меню
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SeedData(); 

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== МЕНЮ КОТОВ ===");
                Console.WriteLine("1. Показать всех котов");
                Console.WriteLine("2. Добавить кота");
                Console.WriteLine("3. Обновить кота");
                Console.WriteLine("4. Удалить кота");
                Console.WriteLine("5. Группировка по породе");
                Console.WriteLine("6. Коты тяжелее N кг");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowAll(); break;
                    case "2": AddCat(); break;
                    case "3": UpdateCat(); break;
                    case "4": DeleteCat(); break;
                    case "5": GroupByBreed(); break;
                    case "6": HeavyCats(); break;
                    case "0": return; 
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                Console.WriteLine("\nНажми Enter, чтобы продолжить...");
                Console.ReadLine();
            }
        }
        /// <summary>
        /// Добавление исходных котов
        /// </summary>
        static void SeedData()
        {
            if (catLogic.GetAllCats().Count == 0)
            {
                catLogic.AddCat("Барсик", "Британская", 3, 5.2, "Серый");
                catLogic.AddCat("Мурка", "Персидская", 5, 3.8, "Белый");
                catLogic.AddCat("Рыжик", "Дворовая", 2, 4.1, "Рыжий");
            }
        }
        /// <summary>
        /// показать всех
        /// </summary>
        static void ShowAll()
        {
            var cats = catLogic.GetAllCats();
            if (cats.Count == 0) { Console.WriteLine("Котов нет."); return; }
            foreach (var c in cats) Console.WriteLine(c);
        }
        /// <summary>
        /// добавить кота
        /// </summary>
        static void AddCat()
        {
            try
            {
                Console.Write("Кличка: "); string name = Console.ReadLine();
                Console.Write("Порода: "); string breed = Console.ReadLine();
                Console.Write("Возраст: "); int age = int.Parse(Console.ReadLine());
                Console.Write("Вес: "); double weight = double.Parse(Console.ReadLine());
                Console.Write("Окрас: "); string color = Console.ReadLine();
                catLogic.AddCat(name, breed, age, weight, color);
                Console.WriteLine("Кот добавлен!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: возраст — целое, вес — число!");
            }
        }
        /// <summary>
        /// обновить кота
        /// </summary>
        static void UpdateCat()
        {
            Console.Write("Id кота для обновления: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Не число!"); return; }
            var cat = catLogic.GetCatById(id);
            if (cat == null) { Console.WriteLine("Кот не найден!"); return; }

            Console.Write($"Новая кличка ({cat.Name}): ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = cat.Name;

            Console.Write($"Новая порода ({cat.Breed}): ");
            string breed = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(breed)) breed = cat.Breed;

            Console.Write($"Новый возраст ({cat.Age}): ");
            string ageStr = Console.ReadLine();
            int age = string.IsNullOrWhiteSpace(ageStr) ? cat.Age : int.Parse(ageStr);

            Console.Write($"Новый вес ({cat.Weight}): ");
            string weightStr = Console.ReadLine();
            double weight = string.IsNullOrWhiteSpace(weightStr) ? cat.Weight : double.Parse(weightStr);

            Console.Write($"Новый окрас ({cat.Color}): ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) color = cat.Color;

            catLogic.UpdateCat(id, name, breed, age, weight, color);
            Console.WriteLine("Обновлено!");

            Console.WriteLine("\n=== ТЕКУЩИЙ СПИСОК КОТОВ ===");
            ShowAll();
        }
        /// <summary>
        /// удалить кота
        /// </summary>
        static void DeleteCat()
        {
            Console.WriteLine("\n=== ТЕКУЩИЙ СПИСОК КОТОВ ===");
            ShowAll();
            Console.Write("Id кота для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (catLogic.DeleteCat(id))
                    Console.WriteLine("Удалён!");
                else
                    Console.WriteLine("Кот не найден!");
            }
        }
        /// <summary>
        /// группировка по породе
        /// </summary>
        static void GroupByBreed()
        {
            var groups = catLogic.GroupByBreed();
            foreach (var g in groups)
            {
                Console.WriteLine($"\nПорода: {g.Key} ({g.Value.Count} шт.)");
                foreach (var c in g.Value) Console.WriteLine("   " + c);
            }
        }
        /// <summary>
        /// поиск тяжелее n кг
        /// </summary>
        static void HeavyCats()
        {
            Console.Write("Минимальный вес: ");
            if (double.TryParse(Console.ReadLine(), out double w))
            {
                var heavy = catLogic.GetCatsHeavierThan(w);
                if (heavy.Count == 0) Console.WriteLine("Нет таких котов.");
                else foreach (var c in heavy) Console.WriteLine(c);
            }
        }
    }
}
