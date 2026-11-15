using System;
using System.Collections.Generic;
using System.Linq;
using Shared;
using Shared.Models;

namespace Presenter
{
    public class CatPresenter
    {
        private readonly ICatView view;
        private readonly ICatModel model;

        public CatPresenter(ICatView view, ICatModel model)
        {
            this.view = view;
            this.model = model;

            // 1. Подписываемся на события View
            view.AddCatEvent += OnAddData;
            view.DeleteCatEvent += OnDeleteData;
            view.UpdateCatEvent += OnUpdateData;

            // 2. Подписываемся на события Model
            model.DataChanged += OnDataChanged;

            // Первичная отрисовка
            OnDataChanged();
        }

        private void OnAddData()
        {
            try
            {
                var cat = new Cat
                {
                    Name = view.GetName(),
                    Breed = view.GetBreed(),
                    Age = view.GetAge(),
                    Weight = view.GetWeight(),
                    Color = view.GetColor()
                };
                model.Insert(cat);
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Ошибка добавления: {ex.Message}");
            }
        }

        private void OnDeleteData(int id)
        {
            if (id > 0) model.Delete(id);
            else view.ShowMessage("Выбери кота!");
        }

        private void OnUpdateData()
        {
            int id = view.GetSelectedId();
            if (id > 0)
            {
                var cat = new Cat
                {
                    Id = id,
                    Name = view.GetName(),
                    Breed = view.GetBreed(),
                    Age = view.GetAge(),
                    Weight = view.GetWeight(),
                    Color = view.GetColor()
                };
                model.Update(cat);
            }
            else view.ShowMessage("Выбери кота для обновления!");
        }

        // Этот метод вызывается, когда модель сообщает об изменениях
        private void OnDataChanged()
        {
            view.RedrawForm(model.GetAll());
        }
    }
}