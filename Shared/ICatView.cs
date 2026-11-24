
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
        /// <summary>
        /// Событие запроса на добавление кота
        /// </summary>
        event EventHandler<CatInputEventArgs> AddCatRequested;
        /// <summary>
        /// Событие запроса на обновление данных кота
        /// </summary>
        event EventHandler<CatInputEventArgs> UpdateCatRequested;
        /// <summary>
        /// Событие запроса на удаление кота
        /// </summary>
        event EventHandler<CatIdEventArgs> DeleteCatRequested;
        /// <summary>
        /// Событие запроса на отображение всех котов
        /// </summary>
        event EventHandler ShowAllCatsRequested;
        /// <summary>
        /// Событие запроса на отображение котов, сгруппированных по породам
        /// </summary>
        event EventHandler ShowBreedGroupsRequested;
        /// <summary>
        /// Событие запроса на отображение котов тяжелее заданного веса
        /// </summary>
        event EventHandler<CatWeightEventArgs> ShowHeavyCatsRequested;
        /// <summary>
        /// Отображает список котов в представлении
        /// </summary>
        /// <param name="cats">список котов</param>
        void ShowCats(IReadOnlyList<CatDto> cats);
        /// <summary>
        /// Отображает котов, сгруппированных по породам
        /// </summary>
        /// <param name="groups">список</param>
        void ShowBreedGroups(IReadOnlyList<BreedGroupDto> groups);
        /// <summary>
        /// Показывает пользователю информационное сообщение
        /// </summary>
        /// <param name="message">иекси сообщение</param>
        void ShowMessage(string message);
        /// <summary>
        /// Показывает пользователю сообщение об ошибке
        /// </summary>
        /// <param name="message">текст ошибки</param>
        void ShowError(string message);
        /// <summary>
        /// Очищает поля ввода в форме
        /// </summary>
        void ClearFields();
    }
}
