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
    public partial class FormSettingDetail : Form
    {
        public FormSettingDetail()
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
                var products = new Products();

                products.ExePath = openFileDialog1.FileName;

                var productsAfter = db.Products.Add(products);

                db.SaveChanges();

                DataGridViewRow row = new DataGridViewRow
                {
                    Height = 35
                };

                DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell
                {
                    Value = productsAfter.ID
                };
                row.Cells.Add(textboxcell);

                DataGridViewTextBoxCell textboxcel2 = new DataGridViewTextBoxCell
                {
                    Value = productsAfter.ProductName
                };
                row.Cells.Add(textboxcel2);

                DataGridViewTextBoxCell textboxcel4 = new DataGridViewTextBoxCell
                {
                    Value = productsAfter.ExePath
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
                var products = db.Products.ToList();
                foreach (var product in products)
                {
                    DataGridViewRow row = new DataGridViewRow
                    {
                        Height = 35
                    };

                    DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell
                    {
                        Value = product.ID
                    };
                    row.Cells.Add(textboxcell);

                    DataGridViewTextBoxCell textboxcel2 = new DataGridViewTextBoxCell
                    {
                        Value = product.ProductName
                    };
                    row.Cells.Add(textboxcel2);

                    DataGridViewTextBoxCell textboxcel4 = new DataGridViewTextBoxCell
                    {
                        Value = product.ExePath
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
                    var product = db.Products.Where(x => x.ID == id).FirstOrDefault();

                    product.ProductName = dataGridView1.Rows[e.RowIndex].Cells["ColumnProductName"].Value.ToString();

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
                    var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
                    db.Products.Remove(product);
                    db.SaveChanges();

                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormSettingDetailProduct formSettingDetailProduct = new FormSettingDetailProduct();
            Products products;
            using (var db = new MyDbContext())
            {
                int id = dataGridView1.Rows[e.RowIndex].Cells["ColumnID"].Value.ToInt();
                products = db.Products.Where(x => x.ID == id).FirstOrDefault();
            }
            formSettingDetailProduct.Products = products;
            formSettingDetailProduct.ShowDialog();
        }
    }
}
