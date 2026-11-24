
using System;
using Shared;
using meow.core.Interfaces;


namespace Presenter
{
    public class CatPresenter
    {
        /// <summary>
        /// Представление View
        /// </summary>
        private readonly ICatView view;
        /// <summary>
        /// Модель бизнес-логики
        /// </summary>
        private readonly ICatLogic model;
        /// <summary>
        /// Инициализирует презентер
        /// </summary>
        /// <param name="view">Представление</param>
        /// <param name="model">Модель бизнес-логики</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CatPresenter(ICatView view, ICatLogic model)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));

            this.model = model ?? throw new ArgumentNullException(nameof(model));

            view.AddCatRequested += OnAddCat;
            view.UpdateCatRequested += OnUpdateCat;
            view.DeleteCatRequested += OnDeleteCat;

            view.ShowAllCatsRequested += OnShowAllCats;
            view.ShowBreedGroupsRequested += OnShowBreedGroups;
            view.ShowHeavyCatsRequested += OnShowHeavyCats;

            model.DataChanged += OnDataChanged;

            RefreshView();
        }
        /// <summary>
        /// Обработчик добавления кота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAddCat(object sender,CatInputEventArgs e)
        {
            try
            {
                if (!Validate(e, out int age, out double weight))
                    return;

                model.AddCat(
                    e.Name.Trim(),
                    e.Breed.Trim(),
                    age,
                    weight,
                    e.Color.Trim());

                view.ShowMessage("Кот успешно добавлен!");
                view.ClearFields();
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// Обработчик обновления кота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUpdateCat(object sender, CatInputEventArgs e)
        {
            try
            {
                if (e.Id <= 0)
                {
                    view.ShowError("Выберите кота!");
                    return;
                }

                if (!Validate(e, out int age, out double weight))
                    return;

                if (!model.UpdateCat(
                    e.Id,
                    e.Name.Trim(),
                    e.Breed.Trim(),
                    age,
                    weight,
                    e.Color.Trim()))
                {
                    view.ShowError("Кот не найден!");
                    return;
                }

                view.ShowMessage("Данные обновлены!");
                view.ClearFields();
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// Обработчик удаления кота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDeleteCat(object sender, CatIdEventArgs e)
        {
            try
            {
                if (e.Id <= 0)
                {
                    view.ShowError("Выберите кота!");
                    return;
                }

                if (!model.DeleteCat(e.Id))
                {
                    view.ShowError("Кот не найден!");
                    return;
                }

                view.ShowMessage("Кот удалён!");
                view.ClearFields();
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// Обработчик запроса на отображение всех котов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowAllCats(object sender, EventArgs e)
        {
            RefreshView();
        }
        /// <summary>
        /// Обработчик запроса на отображение котов, сгруппированных по породам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowBreedGroups(object sender, EventArgs e)
        {
            try
            {
                view.ShowBreedGroups(model.GroupByBreed());
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// Обработчик запроса на отображение котов тяжелее заданного веса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnShowHeavyCats(object sender, CatWeightEventArgs e)
        {
            try
            {
                view.ShowCats(model.GetCatsHeavierThan(e.Weight));
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }
        /// <summary>
        /// Обработчик изменения данных в модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataChanged(object sender, EventArgs e)
        {
            RefreshView();
        }
        /// <summary>
        /// Обновляет представление, загружая полный список котов из модели
        /// </summary>
        private void RefreshView()
        {
            view.ShowCats(model.GetAllCats());
        }
        /// <summary>
        /// Валидирует входные данные формы кота.
        /// Проверяет: непустые текстовые поля, корректный неотрицательный возраст и положительный вес.
        /// </summary>
        /// <param name="e">данные из формы</param>
        /// <param name="age">возраст</param>
        /// <param name="weight">вес</param>
        /// <returns>true, если данные корректны; иначе false</returns>
        private bool Validate(CatInputEventArgs e, out int age, out double weight)
        {
            age = 0;
            weight = 0;

            if (string.IsNullOrWhiteSpace(e.Name) ||
                string.IsNullOrWhiteSpace(e.Breed) ||
                string.IsNullOrWhiteSpace(e.Color))
            {
                view.ShowError("Заполните все текстовые поля!");
                return false;
            }

            if (!int.TryParse(e.Age, out age) || age < 0)
            {
                view.ShowError("Некорректный возраст!");
                return false;
            }

            if (!double.TryParse(
                e.Weight,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.CurrentCulture,
                out weight) || weight <= 0)
            {
                view.ShowError("Некорректный вес!");
                return false;
            }

            return true;
        }
    }
}
