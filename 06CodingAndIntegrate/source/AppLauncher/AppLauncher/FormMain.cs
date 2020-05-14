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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        System.Environment.Exit(0);
                        break;
                    }
            }
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (openApp.ShowDialog(this) == DialogResult.OK)
            {
                PictureBox pictureBox = new PictureBox();

                pictureBox.Tag = openApp.FileName;

                pictureBox.Size = new Size(480, 480);
                pictureBox.BackColor = Color.Black;

                pictureBox.Click += PictureBox_Click;

                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            FormShow formShow = new FormShow();
            formShow.AppFilename = ((PictureBox)sender).Tag.ToString();
            formShow.ShowDialog();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;////用双缓冲绘制窗口的所有子控件
                return cp;
            }
        }
    }
}
