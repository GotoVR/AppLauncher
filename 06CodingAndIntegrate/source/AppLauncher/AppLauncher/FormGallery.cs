using AppLauncher.Data;
using AppLauncher.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLauncher
{
    public partial class FormGallery : Form
    {
        public Products Products;
        public FormGallery()
        {
            InitializeComponent();
        }

        private void FormGallery_Load(object sender, EventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var productsDetails = db.ProductsDetails.Where(x => x.ProductsID == Products.ID).ToList();
                foreach (var productsDetail in productsDetails)
                {
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Tag = productsDetail;

                    pictureBox.Size = new Size(480, 480);
                    pictureBox.BackColor = Color.Black;

                    pictureBox.Click += PictureBox_Click;

                    flowLayoutPanel1.Controls.Add(pictureBox);

                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            ProductsDetails productsDetails = (ProductsDetails)((PictureBox)sender).Tag;

            if (productsDetails.ExePath.Contains(".exe"))
            {
                FormShow formShow = new FormShow();
                formShow.Products = new Products()
                {
                    ExePath = productsDetails.ExePath
                };
                formShow.ShowDialog();
            }
            else
            {
                Process p = Process.Start(productsDetails.ExePath);
            }
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
