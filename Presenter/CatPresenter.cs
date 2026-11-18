using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Presenter
{
    public class CatPresenter
    {
        private readonly ICatView view;
        private readonly ICatModel model;
        /// <summary>
        /// Создает экземпляр презентера. В конструкторе происходит подписка на события View и Model, а также первичная отрисовка данных
        /// </summary>
        /// <param name="view">Реализация интерфейса ICatView</param>
        /// <param name="model">Реализация интерфейса ICatModel</param>
        public CatPresenter(ICatView view, ICatModel model)
        {
            this.view = view;
            this.model = model;

            view.AddCatEvent += OnAddData;
            view.DeleteCatEvent += OnDeleteData;
            view.UpdateCatEvent += OnUpdateData;

            model.DataChanged += OnDataChanged;

            OnDataChanged();
        }
        /// <summary>
        /// Создает объект Cat из данных, введенных пользователем во View, и передает его в Model через метод Insert
        /// </summary>
        private void OnAddData()
        {
            try
            {
                model.Insert(
                    view.GetName(),
                    view.GetBreed(),
                    view.GetAge(),
                    view.GetWeight(),
                    view.GetColor()
                );
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Ошибка добавления: {ex.Message}");
            }
        }
        /// <summary>
        /// Проверяет, что выделен корректный Id, и вызывает метод Delete у Model
        /// </summary>
        /// <param name="id">Идентификатор кота, которого нужно удалить</param>
        private void OnDeleteData(int id)
        {
            if (id > 0) model.Delete(id);
            else view.ShowMessage("Выбери кота!");
        }
        /// <summary>
        /// Создает объект Cat с текущими данными из View и Id выделенного кота, после чего вызывает метод Update у Model
        /// </summary>
        private void OnUpdateData()
        {
            int id = view.GetSelectedId();
            if (id > 0)
            {
                model.Update(
                   id,
                   view.GetName(),
                   view.GetBreed(),
                   view.GetAge(),
                   view.GetWeight(),
                   view.GetColor()
               );
            }
            else view.ShowMessage("Выбери кота для обновления!");
        }
        /// <summary>
        /// Получает актуальный список котов из Model и передает его во View для перерисовки интерфейса
        /// </summary>
        private void OnDataChanged()
        {
            view.RedrawForm(model.GetAll());
        }
    }
}