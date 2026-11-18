
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared
{
    /// <summary>
    /// Интерфейс представления (View). 
    /// Генерирует события для Презентера и умеет перерисовываться.
    /// </summary>
    public interface ICatView
    {
        event Action AddCatEvent;
        event Action<int> DeleteCatEvent;
        event Action UpdateCatEvent;


        string GetName();
        string GetBreed();
        int GetAge();
        double GetWeight();
        string GetColor();
        int GetSelectedId();
        /// <summary>
        /// Перерисовать форму
        /// </summary>
        /// <param name="data">Коллекция котов, которых нужно отобразить</param>
        void RedrawForm(IEnumerable<object> data);
        /// <summary>
        /// Показать информационное сообщение пользователю
        /// </summary>
        /// <param name="message">текст сообщения</param>
        void ShowMessage(string message);
    }
}
