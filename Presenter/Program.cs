//using System;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using meow.console;
//using meow.winforms;

//namespace Presenter
//{
//    /// <summary>
//    /// Композиционный корень приложения.
//    /// Presenter запускается первым, создаёт представление,
//    /// модель и связывает их между собой.
//    /// По умолчанию запускается WinForms,
//    /// с аргументом --console запускается консоль.
//    /// </summary>
//    internal static class Program
//    {
//        [STAThread]
//        private static void Main(string[] args)
//        {
//            bool consoleMode = args.Any(
//                arg => string.Equals(
//                    arg,
//                    "--console",
//                    StringComparison.OrdinalIgnoreCase));

//            if (consoleMode)
//            {
//                Console.OutputEncoding = Encoding.UTF8;
//                Console.InputEncoding = Encoding.UTF8;

//                ConsoleView view = ConsoleViewStarter.CreateView();

//                CatPresenter presenter = CatProgram.Create(view);

//                ConsoleViewStarter.Start(view);
//            }
//            else
//            {
//                Form1 view = WinFormsStarter.CreateForm();

//                CatPresenter presenter = CatProgram.Create(view);

//                WinFormsStarter.StartForm(view);
//            }
//        }
//    }
//}
using System;
using System.Linq;
using System.Text;
using meow.console;
using meow.winforms;

namespace Presenter
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            bool consoleMode = args.Any(arg =>
                string.Equals(
                    arg,
                    "--console",
                    StringComparison.OrdinalIgnoreCase));

            if (consoleMode)
            {
                StartConsole();
            }
            else
            {
                StartWinForms();
            }
        }

        private static void StartConsole()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var view = ConsoleViewStarter.CreateView();

            var presenter = CatProgram.Create(view);

            ConsoleViewStarter.Start(view);

            GC.KeepAlive(presenter);
        }

        private static void StartWinForms()
        {
            var view = WinFormsStarter.CreateForm();

            var presenter = CatProgram.Create(view);

            WinFormsStarter.StartForm(view);

            GC.KeepAlive(presenter);
        }
    }
}