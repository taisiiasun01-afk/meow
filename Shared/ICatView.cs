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
    internal interface ICatView
    {
        event Action AddCatEvent;
        event Action<int> DeleteCatEvent;
        event Action UpdateCatEvent;

        void RedrawForm(IEnumerable<object> data);
        void ShowMessage(string message);

        string GetName();
        string GetBreed();
        int GetAge();
        double GetWeight();
        string GetColor();
        int GetSelectedId();
    }
}
