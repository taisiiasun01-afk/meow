
namespace meow.console
{
    /// <summary>
    /// Точка запуска консольного представления.
    /// </summary>
    public static class ConsoleViewStarter
    {
        /// <summary>
        /// Создаёт консольное представление.
        /// </summary>
        public static ConsoleView CreateView()
        {
            return new ConsoleView();
        }

        /// <summary>
        /// Запускает консольное меню.
        /// </summary>
        public static void Start(ConsoleView view)
        {
            view.Run();
        }
    }
}