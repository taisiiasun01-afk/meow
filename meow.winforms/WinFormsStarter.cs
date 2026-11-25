using System;
using System.Windows.Forms;

namespace meow.winforms
{
    public static class WinFormsStarter
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }


          /// <summary>
          /// Создаёт главную форму.
          /// </summary>
        public static Form1 CreateForm()
        {
            return new Form1();
        }

        /// <summary>
        /// Запускает форму.
        /// </summary>
        public static void StartForm(Form1 form)
        {
            Application.Run(form);
        }
    }
}
