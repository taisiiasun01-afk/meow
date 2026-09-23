using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using meow.core.Logic;
using meow.core.Models;

namespace meow.winforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private CatLogic catLogic = new CatLogic();
        /// <summary>
        /// кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string breed = txtBreed.Text;
                int age = int.Parse(txtAge.Text);
                double weight = double.Parse(txtWeight.Text);
                string color = txtColor.Text;

                catLogic.AddCat(name, breed, age, weight, color);
                RefreshGrid();
                ClearFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Возраст - целое число, вес — число с точкой (например, 6.7)");
            }
        }
        /// <summary>
        /// кнопка обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выдели кота в таблице!");
                return;
            }

            try
            {
                var selected = (Cat)dataGridView1.CurrentRow.DataBoundItem;
                catLogic.UpdateCat(selected.Id,
                    txtName.Text, txtBreed.Text,
                    int.Parse(txtAge.Text),
                    double.Parse(txtWeight.Text),
                    txtColor.Text);

                RefreshGrid();
                ClearFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверь формат ввода!");
            }
        }
        /// <summary>
        /// кнопка удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Выдели кота!");
                return;
            }

            var selected = (Cat)dataGridView1.CurrentRow.DataBoundItem;
            catLogic.DeleteCat(selected.Id);
            RefreshGrid();
            ClearFields();
        }
        /// <summary>
        /// заглушка от клика по таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        /// <summary>
        /// кнопка показать все
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();

        }
        /// <summary>
        /// кнопка группировка по породе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroup_Click(object sender, EventArgs e)
        {
            var groups = catLogic.GroupByBreed();
            string result = "";
            foreach (var g in groups)
            {
                result += $"Порода: {g.Key} ({g.Value.Count} шт.)\n";
                foreach (var cat in g.Value)
                    result += "   " + cat + "\n";
                result += "\n";
            }
            MessageBox.Show(result, "Группировка по породе");
        }
        /// <summary>
        /// кнопка тяжелее чем n кг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeavy_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtHeavyWeight.Text, out double minWeight))
            {
                var heavy = catLogic.GetCatsHeavierThan(minWeight);
                string result = heavy.Count == 0
                    ? "Нет котов тяжелее " + minWeight
                    : string.Join("\n", heavy);
                MessageBox.Show(result, "Тяжёлые коты");
            }
        }
        /// <summary>
        /// обновить таблицу
        /// </summary>
        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = catLogic.GetAllCats();
        }
        /// <summary>
        /// очистить поля ввывода
        /// </summary>
        private void ClearFields()
        {
            txtName.Text = "";
            txtBreed.Text = "";
            txtAge.Text = "";
            txtWeight.Text = "";
            txtColor.Text = "";
            txtHeavyWeight.Text = "";
        }
        /// <summary>
        /// загруска формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            SeedData();
            RefreshGrid();
        }
        /// <summary>
        /// добавление исходных котов
        /// </summary>
        private void SeedData()
        {
            if (catLogic.GetAllCats().Count == 0)
            {
                catLogic.AddCat("Барсик", "Британская", 3, 5.2, "Серый");
                catLogic.AddCat("Мурка", "Персидская", 5, 3.8, "Белый");
                catLogic.AddCat("Рыжик", "Дворовая", 2, 4.1, "Рыжий");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }

}
