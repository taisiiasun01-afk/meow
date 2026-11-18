using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Shared;
using meow.core.Models;

namespace meow.winforms
{
    public partial class Form1 : Form, ICatView
    {
        public event Action AddCatEvent;
        public event Action<int> DeleteCatEvent;
        public event Action UpdateCatEvent;
        public Form1()
        {
            InitializeComponent();
        }

        public string GetName() => txtName.Text;
        public string GetBreed() => txtBreed.Text;
        public int GetAge() => int.TryParse(txtAge.Text, out int a) ? a : 0;
        public double GetWeight() => double.TryParse(txtWeight.Text, out double w) ? w : 0;
        public string GetColor() => txtColor.Text;
        public int GetSelectedId()
        {
            if (dataGridView1.CurrentRow == null)
                return 0;

            var cat = dataGridView1.CurrentRow.DataBoundItem as Cat;
            if (cat == null)
                return 0;

            return cat.Id;
        }

        /// <summary>
        /// кнопка добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (AddCatEvent != null)
            {
                AddCatEvent();
            }
        }
        /// <summary>
        /// кнопка обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UpdateCatEvent != null)
            {
                UpdateCatEvent();
            }
        }
        /// <summary>
        /// кнопка удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteCatEvent != null)
            {
                DeleteCatEvent(GetSelectedId());
            }
        }

        /// <summary>
        /// кнопка показать все
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowAll_Click(object sender, EventArgs e)
        {
           

        }
        /// <summary>
        /// кнопка группировка по породе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGroup_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// кнопка тяжелее чем n кг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHeavy_Click(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// обновить таблицу
        /// </summary>
        public void RedrawForm(IEnumerable<object> data)
        {
            dataGridView1.DataSource = null;
            // Приводим object обратно к Cat
            dataGridView1.DataSource = data.Cast<Cat>().ToList();
        }
        ///// <summary>
        ///// очистить поля ввывода
        ///// </summary>
        //private void ClearFields()
        //{
        //    txtName.Text = "";
        //    txtBreed.Text = "";
        //    txtAge.Text = "";
        //    txtWeight.Text = "";
        //    txtColor.Text = "";
        //    txtHeavyWeight.Text = "";
        //}
        /// <summary>
        /// загруска формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// добавление исходных котов
        /// </summary>
        
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }

}
