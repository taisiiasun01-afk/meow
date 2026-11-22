using System;
using System.Windows.Forms;
using Presenter;
using Shared;

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


            var presenter = CatProgram.Create(view);


            Application.Run(view);
        }
    }
}
