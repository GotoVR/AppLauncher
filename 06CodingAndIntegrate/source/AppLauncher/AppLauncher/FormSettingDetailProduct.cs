using AppLauncher.Data;
using AppLauncher.Models;
using HZH_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLauncher
{
    public partial class FormSettingDetailProduct : Form
    {
        public Products Products;

        public FormSettingDetailProduct()
        {
            InitializeComponent();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var productsDetails = new ProductsDetails();

                productsDetails.ProductsID = Products.ID;
                productsDetails.ExePath = openFileDialog1.FileName;

                var productsDetailsAfter = db.ProductsDetails.Add(productsDetails);

                db.SaveChanges();

                DataGridViewRow row = new DataGridViewRow
                {
                    Height = 35
                };

                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell
                {
                    Value = productsDetailsAfter.ID
                };
                row.Cells.Add(textboxcell);

                DataGridViewTextBoxCell textboxcel2 = new DataGridViewTextBoxCell
                {
                    Value = productsDetailsAfter.ProductsDetailsName
                };
                row.Cells.Add(textboxcel2);

                DataGridViewTextBoxCell textboxcel4 = new DataGridViewTextBoxCell
                {
                    Value = productsDetailsAfter.ExePath
                };
                row.Cells.Add(textboxcel4);

                DataGridViewButtonCell buttonCell1 = new DataGridViewButtonCell
                {
                    FlatStyle = FlatStyle.Flat
                };
                row.Cells.Add(buttonCell1);

                DataGridViewButtonCell buttonCell2 = new DataGridViewButtonCell
                {
                    FlatStyle = FlatStyle.Flat
                };
                row.Cells.Add(buttonCell2);

                DataGridViewButtonCell buttonCell3 = new DataGridViewButtonCell
                {
                    FlatStyle = FlatStyle.Flat
                };
                row.Cells.Add(buttonCell3);

                dataGridView1.Rows.Add(row);
            }
        }

        private void FormSettingDetail_Load(object sender, EventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var productsDetails = db.ProductsDetails.Where(x => x.ProductsID == Products.ID).ToList();
                foreach (var productsDetail in productsDetails)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        Height = 35
                    };

                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell
                    {
                        Value = productsDetail.ID
                    };
                    row.Cells.Add(textboxcell);

                    DataGridViewTextBoxCell textboxcel2 = new DataGridViewTextBoxCell
                    {
                        Value = productsDetail.ProductsDetailsName
                    };
                    row.Cells.Add(textboxcel2);

                    DataGridViewTextBoxCell textboxcel4 = new DataGridViewTextBoxCell
                    {
                        Value = productsDetail.ExePath
                    };
                    row.Cells.Add(textboxcel4);

                    DataGridViewButtonCell buttonCell1 = new DataGridViewButtonCell
                    {
                        FlatStyle = FlatStyle.Flat
                    };
                    row.Cells.Add(buttonCell1);

                    DataGridViewButtonCell buttonCell2 = new DataGridViewButtonCell
                    {
                        FlatStyle = FlatStyle.Flat
                    };
                    row.Cells.Add(buttonCell2);

                    DataGridViewButtonCell buttonCell3 = new DataGridViewButtonCell
                    {
                        FlatStyle = FlatStyle.Flat
                    };
                    row.Cells.Add(buttonCell3);

                    dataGridView1.Rows.Add(row);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                //更新操作
                using (var db = new MyDbContext())
                {
                    int id = dataGridView1.Rows[e.RowIndex].Cells["ColumnID"].Value.ToInt();
                    var productsDetails = db.ProductsDetails.Where(x => x.ID == id).FirstOrDefault();

                    productsDetails.ProductsDetailsName = dataGridView1.Rows[e.RowIndex].Cells["ColumnProductName"].Value.ToString();

                    db.SaveChanges();
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //删除操作
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ColumnDelete")
            {
                using (var db = new MyDbContext())
                {
                    int id = dataGridView1.Rows[e.RowIndex].Cells["ColumnID"].Value.ToInt();
                    var productsDetails = db.ProductsDetails.Where(x => x.ID == id).FirstOrDefault();
                    db.ProductsDetails.Remove(productsDetails);
                    db.SaveChanges();

                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
        }
    }
}
