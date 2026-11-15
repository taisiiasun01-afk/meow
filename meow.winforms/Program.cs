using System;
using System.Windows.Forms;
using Presenter;
using meow.core.Logic;
using DataAccessLayer.EF;
using Shared;
using Shared.Models;
namespace meow.winforms
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            

            var view = new Form1();

            
            var repo = new EntityRepository<Cat>();
            var service = new CatService(repo);
            ICatModel model = new CatLogic(service);

            
            var presenter = new CatPresenter(view, model);

            
            Application.Run(view);
        }
    }
}
