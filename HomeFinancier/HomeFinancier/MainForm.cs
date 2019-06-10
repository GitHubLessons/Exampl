﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeFinancier
{
    public partial class MainForm : Form
    {
        SqlConnection sqlConnection;
        List<Spending> filterData;
        List<DataCategory> dataforGrafik;



        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
          
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
        }

        private void btnFiller_Click(object sender, EventArgs e)
        {

            float Summ = 0;
            if (dtpBegin.Value <= dtpEnd.Value)
            {
                filterData = new List<Spending>();
                if (cmbCatecory.Text == string.Empty)
                {

                    for (int i = 0; i < Financier.spendingList.Count; i++)
                    {

                        if (Financier.spendingList[i].Date >= dtpBegin.Value & Financier.spendingList[i].Date <= dtpEnd.Value)
                        {
                            filterData.Add(Financier.spendingList[i]);
                            Summ += Financier.spendingList[i].Summ;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < Financier.spendingList.Count; i++)
                    {

                        if ((Financier.spendingList[i].Date >= dtpBegin.Value & Financier.spendingList[i].Date <= dtpEnd.Value) & (Financier.spendingList[i].Category == cmbCatecory.Text))
                        {
                            filterData.Add(Financier.spendingList[i]);
                            Summ += Financier.spendingList[i].Summ;
                        }
                    }
                }

                FillFillterTable(Summ);
                label4.Text = "Всего: " + Summ.ToString() + "руб.";
                GetCategoryForGrafic();

            }

        }

        private async void btnAddCategory_Click(object sender, EventArgs e)
        {
           
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dtpBegin.Value = date;
            dtpEnd.Value = date;
            label4.Text = "";
          

            try
            {
                sqlConnection = new SqlConnection(Properties.Settings.Default.conString);
                await sqlConnection.OpenAsync();//открываем соединение с базой данных 
            }
            catch (Exception ezp)
            {
                MessageBox.Show(ezp.Message);
            }
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Category]", sqlConnection);
           try { 
           
           
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    string toAdd = Convert.ToString(sqlReader["Name"]).Trim();
                    // Financier.Categories.Add(Convert.ToString(sqlReader["Name"]));
                    Financier.Categories.Add(toAdd);
                    MessageBox.Show(Convert.ToString(sqlReader["Name"]));

                }
                sqlReader.Close();
                sqlReader = null;
                command = new SqlCommand("SELECT * FROM [Spending]", sqlConnection);
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    Spending newSpending = new Spending();
                   newSpending.SetId(Convert.ToInt32( sqlReader["id"]));
                    newSpending.Value = Convert.ToString(sqlReader["Name"]).Trim();
                    newSpending.Summ = float.Parse(Convert.ToString( sqlReader["Summ"]));
                    newSpending.Quantity = float.Parse(Convert.ToString( sqlReader["Quantity"]));
                    newSpending.SetCategory (Convert.ToString(sqlReader["Category"]).Trim());
                    newSpending.Date = Convert.ToDateTime(sqlReader["Date"]);
                    Financier.spendingList.Add(newSpending);
                   
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
            FillSpendingTable();
            FillComboBoxItem();
            FillCategoryTable();
        } 


        void FillSpendingTable()
        {
            dgvSpending.Rows.Clear();
            for(int i= 0;i<Financier.spendingList.Count;i++)
            {
                dgvSpending.Rows.Add(Financier.spendingList[i].ID, Financier.spendingList[i].Date.ToShortDateString(), Financier.spendingList[i].Value, Financier.spendingList[i].Summ, Financier.spendingList[i].Quantity, Financier.spendingList[i].Category);
            }
        }

        private void cmbCatecory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        void FillFillterTable(float summ)
        {
            dgvFillterData.Rows.Clear();
            for (int i = 0; i < filterData.Count; i++)
            {
                double res = filterData[i].Summ * 100 / summ;
                dgvFillterData.Rows.Add(filterData[i].Date.ToShortDateString(), filterData[i].Category, filterData[i].Summ, res);
            }
        }

        void FillComboBoxItem()
        {
            for (int i = 0; i < Financier.Categories.Count; i++)

            {
                cmbCatecory.Items.Add(Financier.Categories[i]);
            }
        }

        void FillCategoryTable()
        {
            dgvCategorys.Rows.Clear();
            for(int i=0;i<Financier.Categories.Count;i++)
            {
                dgvCategorys.Rows.Add(Financier.Categories[i]);
            }
        }

        void GetCategoryForGrafic()
        {
            chartSpending.Series[0].Points.Clear();
            dataforGrafik = new List<DataCategory>();
            for (int i = 0; i < filterData.Count; i++)
            {
                if (dataforGrafik.Count == 0)
                {
                    dataforGrafik.Add(new DataCategory(filterData[i].Category, filterData[i].Summ));
                }
                else
                {
                    bool find = false;
                    for (int j = 0; j < dataforGrafik.Count; j++)
                    {
                        if (dataforGrafik[j].Name == filterData[i].Category)
                        {
                            dataforGrafik[j].Summ += filterData[i].Summ;
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        dataforGrafik.Add(new DataCategory(filterData[i].Category, filterData[i].Summ));
                    }
                }


            }

            for (int i = 0; i < dataforGrafik.Count; i++)
            {
                chartSpending.Series[0].Points.AddY(Convert.ToDouble(dataforGrafik[i].Summ));
                chartSpending.Series[0].Points[i].LegendText = dataforGrafik[i].Name;
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            dtpBegin.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            cmbCatecory.Text = string.Empty;
            dgvFillterData.Rows.Clear();
            chartSpending.Series[0].Points.Clear();
        }
    }
}
