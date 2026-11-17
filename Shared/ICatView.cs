using Shared.Models;
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

        void RedrawForm(IEnumerable<Cat> data);
        void ShowMessage(string message);
    }
}
