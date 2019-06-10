using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeFinancier
{
    public partial class FAddChange : Form
    {
     
        public FAddChange()
        {
            InitializeComponent();
        }

        private void FAddChange_Load(object sender, EventArgs e)
        {
            FillComboBoxItem();
            dtp.Value = Financier.CurrentSpending.Date;
            txtbName.Text = Financier.CurrentSpending.Value;
            txtbSumm.Text = Financier.CurrentSpending.Summ.ToString();
            txtbQuanlity.Text = Financier.CurrentSpending.Quantity.ToString();
            comboBox1.Text = Financier.CurrentSpending.Category;

        }

        private void btnAddChange_Click(object sender, EventArgs e)
        {
            float summ = 0, quanlity = 0;
            if (txtbName.Text == string.Empty | txtbQuanlity.Text == string.Empty | txtbSumm.Text == string.Empty | comboBox1.Text == string.Empty
                | !float.TryParse(txtbSumm.Text, out summ) | !float.TryParse(txtbQuanlity.Text, out quanlity))
            {
                MessageBox.Show("Должны быть заполнены все поля!");
            }
            else
            {
                Financier.CurrentSpending.Value = txtbName.Text;
                Financier.CurrentSpending.Date = dtp.Value;
                Financier.CurrentSpending.Summ = summ;
                Financier.CurrentSpending.Quantity = quanlity;
                Financier.CurrentSpending.SetCategory(comboBox1.Text);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }


        void FillComboBoxItem()
        {
           
        }
    }
}
