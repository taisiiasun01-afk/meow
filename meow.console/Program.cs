using System;
using DataAccessLayer.EF; 
using meow.core.Logic;
using meow.core.Models;
using Presenter;
using Shared;

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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            using (var context = new CatsContext())
            {
                context.Database.EnsureCreated();
            }

            
            var view = new ConsoleView();
            var repo = new EntityRepository<Cat>();
            var service = new CatService(repo);
            ICatModel model = new CatLogic(service);
            var presenter = new CatPresenter(view, model);

           
            Console.ReadKey();
        }
    }
}