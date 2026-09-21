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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            RefreshGrid();

        }

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
        private void RefreshGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = catLogic.GetAllCats();
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtBreed.Text = "";
            txtAge.Text = "";
            txtWeight.Text = "";
            txtColor.Text = "";
            txtHeavyWeight.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SeedData();
            RefreshGrid();
        }
        private void SeedData()
        {
            if (catLogic.GetAllCats().Count == 0)
            {
                catLogic.AddCat("Барсик", "Британская", 3, 5.2, "Серый");
                catLogic.AddCat("Мурка", "Персидская", 5, 3.8, "Белый");
                catLogic.AddCat("Рыжик", "Дворовая", 2, 4.1, "Рыжий");
            }
        }
    }

}
