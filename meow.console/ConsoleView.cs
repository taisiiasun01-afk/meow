using meow.core.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;


namespace meow.console
{
    /// <summary>
    /// Консольное представление (View) для работы с котами
    /// Реализует интерфейс ICatView для работы через Presenter
    /// </summary>
    public class ConsoleView : ICatView
    {
        public event Action AddCatEvent;
        public event Action<int> DeleteCatEvent;
        public event Action UpdateCatEvent;

        private string _name;
        private string _breed;
        private int _age;
        private double _weight;
        private string _color;
        private int _selectedId;

        public string GetName() => _name;
        public string GetBreed() => _breed;
        public int GetAge() => _age;
        public double GetWeight() => _weight;
        public string GetColor() => _color;
        public int GetSelectedId() => _selectedId;
        /// <summary>
        /// Отобразить список котов в консоли и показать главное меню
        /// </summary>
        /// <param name="data">Коллекция объектов, которую нужно отобразить</param>
        public void RedrawForm(IEnumerable<object> data)
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК КОТОВ ===");
            var cats = data.Cast<Cat>().ToList();
            if (cats.Count == 0)
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
            Console.WriteLine("====================");
            ShowMenu();
        }
        /// <summary>
        /// Показать информационное сообщение пользователю
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }
        /// <summary>
        /// Показать главное меню и обработать выбор пользователя
        /// Генерирует соответствующие события для Presenter
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Обновить");
            Console.WriteLine("3. Удалить");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Кличка: "); _name = Console.ReadLine();
                    Console.Write("Порода: "); _breed = Console.ReadLine();
                    Console.Write("Возраст: ");
                    int.TryParse(Console.ReadLine(), out _age);
                    Console.Write("Вес: "); double.TryParse(Console.ReadLine(), out _weight);
                    Console.Write("Окрас: "); _color = Console.ReadLine();
                    AddCatEvent?.Invoke();
                    break;
                case "2":
                    Console.Write("Id кота: "); int.TryParse(Console.ReadLine(), out _selectedId);
                    Console.Write("Новая кличка: "); _name = Console.ReadLine();
                    Console.Write("Новая порода: "); _breed = Console.ReadLine();
                    Console.Write("Новый возраст: "); int.TryParse(Console.ReadLine(), out _age);
                    Console.Write("Новый вес: "); double.TryParse(Console.ReadLine(), out _weight);
                    Console.Write("Новый окрас: "); _color = Console.ReadLine();
                    UpdateCatEvent?.Invoke();
                    break;
                case "3":
                    Console.Write("Id кота: "); int.TryParse(Console.ReadLine(), out _selectedId);
                    DeleteCatEvent?.Invoke(_selectedId);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }
}
