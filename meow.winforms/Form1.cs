using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace meow.winforms
{
    public partial class Form1 : Form, ICatView
    {
        public event EventHandler<CatInputEventArgs> AddCatRequested;
        public event EventHandler<CatInputEventArgs> UpdateCatRequested;
        public event EventHandler<CatIdEventArgs> DeleteCatRequested;

        public event EventHandler ShowAllCatsRequested;
        public event EventHandler ShowBreedGroupsRequested;
        public event EventHandler<CatWeightEventArgs> ShowHeavyCatsRequested;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ShowAllCatsRequested?.Invoke(this, EventArgs.Empty);
        }

        private int GetSelectedId()
        {
            if (dataGridView1.CurrentRow == null)
                return 0;

            var cat = dataGridView1.CurrentRow.DataBoundItem as CatDto;

            return cat == null ? 0 : cat.Id;
        }

        private CatInputEventArgs ReadInput()
        {
            return new CatInputEventArgs(
                GetSelectedId(),
                txtName.Text,
                txtBreed.Text,
                txtAge.Text,
                txtWeight.Text,
                txtColor.Text);
        }


        /// <summary>
        /// кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCatRequested?.Invoke(this, ReadInput());
        }
        /// <summary>
        /// кнопка обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCatRequested?.Invoke(this, ReadInput());
        }
        /// <summary>
        /// кнопка удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCatRequested?.Invoke(
                            this,
                            new CatIdEventArgs(GetSelectedId()));
        }

        /// <summary>
        /// кнопка показать все
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ShowAllCatsRequested?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// кнопка группировка по породе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroup_Click(object sender, EventArgs e)
        {
            ShowBreedGroupsRequested?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// кнопка тяжелее чем n кг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeavy_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(
                txtHeavyWeight.Text,
                out double weight))
            {
                ShowError("Введите корректный вес!");
                return;
            }

            ShowHeavyCatsRequested?.Invoke(
                this,
                new CatWeightEventArgs(weight));
        }
        public void ShowCats(IReadOnlyList<CatDto> cats)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cats.ToList();
        }

        public void ShowBreedGroups(
            IReadOnlyList<BreedGroupDto> groups)
        {
            if (groups.Count == 0)
            {
                ShowMessage("Нет данных для группировки.");
                return;
            }

            StringBuilder result = new StringBuilder();

            foreach (var group in groups)
            {
                result.AppendLine("Порода: " + group.Breed);
                result.AppendLine("Количество: " + group.Count);

                foreach (var cat in group.Cats)
                {
                    result.AppendLine("  " + cat.ToString());
                }

                result.AppendLine();
            }

            MessageBox.Show(
                this,
                result.ToString(),
                "Группировка котов по породам",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(
                this,
                message,
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(
                this,
                message,
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public void ClearFields()
        {
            txtName.Clear();
            txtBreed.Clear();
            txtAge.Clear();
            txtWeight.Clear();
            txtColor.Clear();
        }


    }
}




