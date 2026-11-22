
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared
{
    ///
    /// Это интерфейс представления. Presenter будет работать с формой исключительно через него.
    /// 
    /// <summary>
    /// Интерфейс представления (View). 
    /// Генерирует события для Презентера и умеет перерисовываться.
    /// </summary>
    public interface ICatView
    {

        event EventHandler<CatInputEventArgs> AddCatRequested;
        event EventHandler<CatInputEventArgs> UpdateCatRequested;
        event EventHandler<CatIdEventArgs> DeleteCatRequested;

        event EventHandler ShowAllCatsRequested;
        event EventHandler ShowBreedGroupsRequested;
        event EventHandler<CatWeightEventArgs> ShowHeavyCatsRequested;

        void ShowCats(IReadOnlyList<CatDto> cats);
        void ShowBreedGroups(IReadOnlyList<BreedGroupDto> groups);

        void ShowMessage(string message);
        void ShowError(string message);
        void ClearFields();
    }
}
