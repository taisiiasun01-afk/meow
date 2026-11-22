
using System;
using System.Collections.Generic;
using System.Globalization;
using Shared;

namespace meow.console
{
    public class ConsoleView : ICatView
    {
        public event EventHandler<CatInputEventArgs> AddCatRequested;
        public event EventHandler<CatInputEventArgs> UpdateCatRequested;
        public event EventHandler<CatIdEventArgs> DeleteCatRequested;

        public event EventHandler ShowAllCatsRequested;
        public event EventHandler ShowBreedGroupsRequested;
        public event EventHandler<CatWeightEventArgs> ShowHeavyCatsRequested;

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("========== МЕНЮ ==========");
                Console.WriteLine("1. Добавить кота");
                Console.WriteLine("2. Обновить кота");
                Console.WriteLine("3. Удалить кота");
                Console.WriteLine("4. Показать всех котов");
                Console.WriteLine("5. Группировка по породе");
                Console.WriteLine("6. Найти котов тяжелее заданного веса");
                Console.WriteLine("0. Выход");
                Console.WriteLine("==========================");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCat();
                        break;

                    case "2":
                        UpdateCat();
                        break;

                    case "3":
                        DeleteCat();
                        break;

                    case "4":
                        ShowAllCatsRequested?.Invoke(
                            this, EventArgs.Empty);
                        break;

                    case "5":
                        ShowBreedGroupsRequested?.Invoke(
                            this, EventArgs.Empty);
                        break;

                    case "6":
                        FindHeavyCats();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Программа завершена.");
                        break;

                    default:
                        ShowError("Неверный пункт меню!");
                        break;
                }
            }
        }

        private void AddCat()
        {
            Console.WriteLine("\n--- ДОБАВЛЕНИЕ КОТА ---");

            var input = ReadCatInput(0);

            if (input != null)
                AddCatRequested?.Invoke(this, input);
        }

        private void UpdateCat()
        {
            Console.WriteLine("\n--- ОБНОВЛЕНИЕ КОТА ---");

            int id = ReadId();

            if (id <= 0)
                return;

            var input = ReadCatInput(id);

            if (input != null)
                UpdateCatRequested?.Invoke(this, input);
        }

        private void DeleteCat()
        {
            Console.WriteLine("\n--- УДАЛЕНИЕ КОТА ---");

            int id = ReadId();

            if (id > 0)
            {
                DeleteCatRequested?.Invoke(
                    this,
                    new CatIdEventArgs(id));
            }
        }

        private void FindHeavyCats()
        {
            Console.Write("\nВведите минимальный вес: ");

            if (!double.TryParse(
                Console.ReadLine(),
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out double weight) || weight < 0)
            {
                ShowError("Некорректный вес!");
                return;
            }

            ShowHeavyCatsRequested?.Invoke(
                this,
                new CatWeightEventArgs(weight));
        }

        private CatInputEventArgs ReadCatInput(int id)
        {
            Console.Write("Кличка: ");
            string name = Console.ReadLine();

            Console.Write("Порода: ");
            string breed = Console.ReadLine();

            Console.Write("Возраст: ");
            string age = Console.ReadLine();

            Console.Write("Вес: ");
            string weight = Console.ReadLine();

            Console.Write("Окрас: ");
            string color = Console.ReadLine();

            return new CatInputEventArgs(
                id,
                name,
                breed,
                age,
                weight,
                color);
        }

        private int ReadId()
        {
            Console.Write("Введите Id кота: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                ShowError("Некорректный Id!");
                return 0;
            }

            return id;
        }

        public void ShowCats(IReadOnlyList<CatDto> cats)
        {
            Console.WriteLine();
            Console.WriteLine("========== СПИСОК КОТОВ ==========");

            if (cats == null || cats.Count == 0)
            {
                Console.WriteLine("Котов нет.");
            }
            else
            {
                foreach (var cat in cats)
                {
                    Console.WriteLine(cat);
                }
            }

            Console.WriteLine("==================================");
        }

        public void ShowBreedGroups(
            IReadOnlyList<BreedGroupDto> groups)
        {
            Console.WriteLine();
            Console.WriteLine("======= ГРУППИРОВКА ПО ПОРОДЕ =======");

            if (groups == null || groups.Count == 0)
            {
                Console.WriteLine("Нет данных.");
                return;
            }

            foreach (var group in groups)
            {
                Console.WriteLine();
                Console.WriteLine("Порода: " + group.Breed);
                Console.WriteLine("Количество: " + group.Count);

                foreach (var cat in group.Cats)
                {
                    Console.WriteLine("  " + cat);
                }
            }

            Console.WriteLine("====================================");
        }

        public void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[ИНФОРМАЦИЯ] " + message);
            Console.ResetColor();
        }

        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ОШИБКА] " + message);
            Console.ResetColor();
        }

        public void ClearFields()
        {
        }
    }
}
