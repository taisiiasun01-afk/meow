using System;
using System.Text;
using Presenter;

namespace meow.console
{
    class Program
    {
        /// <summary>
        /// Главный метод приложения
        /// </summary>
        /// <param name="args">аргументы командной строки</param>

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var view = new ConsoleView();

            var presenter = CatProgram.Create(view);

            view.Run();
        }
    }
}