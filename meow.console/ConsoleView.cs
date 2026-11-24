
using System;
using System.Collections.Generic;
using System.Globalization;
using Shared;

namespace meow.console
{
    public class ConsoleView : ICatView
    {
        /// <summary>
        /// пользователь запросил добавление нового кота
        /// </summary>
        public event EventHandler<CatInputEventArgs> AddCatRequested;
        /// <summary>
        /// пользователь запросил обновление данных кота
        /// </summary>
        public event EventHandler<CatInputEventArgs> UpdateCatRequested;
        /// <summary>
        /// пользователь запросил удаление кота по Id
        /// </summary>
        public event EventHandler<CatIdEventArgs> DeleteCatRequested;
        /// <summary>
        /// пользователь запросил отображение всех котов
        /// </summary>
        public event EventHandler ShowAllCatsRequested;
        /// <summary>
        /// пользователь запросил группировку котов по породе
        /// </summary>
        public event EventHandler ShowBreedGroupsRequested;
        /// <summary>
        /// пользователь запросил поиск котов тяжелее указанного веса
        /// </summary>
        public event EventHandler<CatWeightEventArgs> ShowHeavyCatsRequested;
        /// <summary>
        /// работа консоли
        /// </summary>
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
        /// <summary>
        /// Запрашивает у пользователя данные для добавления кота и генерирует событие
        /// </summary>
        private void AddCat()
        {
            Console.WriteLine("\n--- ДОБАВЛЕНИЕ КОТА ---");

            var input = ReadCatInput(0);

            if (input != null)
                AddCatRequested?.Invoke(this, input);
        }
        /// <summary>
        /// Запрашивает Id и новые данные кота, генерирует событие обновления
        /// </summary>
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
        /// <summary>
        /// Запрашивает Id кота и генерирует событие удаления
        /// </summary>
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
        /// <summary>
        /// Запрашивает минимальный вес и генерирует событие поиска тяжелых котов
        /// </summary>
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
        /// <summary>
        /// Читает с консоли данные о коте для добавления или обновления
        /// </summary>
        /// <param name="id">айди</param>
        /// <returns>объект с введенными данными</returns>
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
        /// <summary>
        /// Читает Id кота с консоли и проверяет его корректность
        /// </summary>
        /// <returns>Введенный Id</returns>
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
        /// <summary>
        /// Отображает список котов в консоли
        /// </summary>
        /// <param name="cats">список котов</param>
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
        /// <summary>
        /// Отображает сгруппированный по породам список котов
        /// </summary>
        /// <param name="groups">список групп</param>
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
        /// <summary>
        /// Отображает информационное сообщение
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public void ShowMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[ИНФОРМАЦИЯ] " + message);
            Console.ResetColor();
        }
        /// <summary>
        /// Отображает сообщение об ошибке
        /// </summary>
        /// <param name="message">текст ошибки</param>
        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n[ОШИБКА] " + message);
            Console.ResetColor();
        }
        /// <summary>
        /// Очищает поля ввода
        /// </summary>
        public void ClearFields()
        {
        }
    }
}
