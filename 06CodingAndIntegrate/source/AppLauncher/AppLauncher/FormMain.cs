using AppLauncher.Data;
using AppLauncher.Models;
using AppLauncher.Properties;
using HZH_Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using IniParser;
using IniParser.Model;
using System.IO;
using AutoUpdaterDotNET;

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

        private void PictureBox_DoubleClick(object sender, EventArgs e)
        {
            pictureBox = (PictureBox)sender;
            Products products = (Products)pictureBox.Tag;

            if (products.ExePath.Contains(".exe"))
            {
                FormShow formShow = new FormShow();
                formShow.Products = products;
                formShow.ShowDialog();
            }
            else
            {
                bool bPic = false;
                using (var db = new MyDbContext())
                {
                    var productsDetails = db.ProductsDetails.Where(x => x.ProductsID == products.ID).ToList();

                    if (productsDetails != null && productsDetails.Count > 0)
                    {
                        bPic = true;
                    }
                }
                if (bPic == true)
                {
                    FormGallery formGallery = new FormGallery();
                    formGallery.Products = products;
                    formGallery.ShowDialog();
                }
                else
                {
                    Process p = Process.Start(products.ExePath);
                }
            }
        }

        private PictureBox pictureBox = null;

        private void PictureBox_Click(object sender, EventArgs e)
        {
            pictureBox = (PictureBox)sender;
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

        private void button_admin_Click(object sender, EventArgs e)
        {
            FormSetting formSetting = new FormSetting();
            formSetting.Show();
        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            FormTips formTips = new FormTips();
            formTips.Title = "确定重启电脑吗？";
            formTips.Msg = "确定重启电脑吗？";
            formTips.Pic = Resources.警告;
            formTips.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string urlSave = @"http://applauncher.cuangeju.cn/AppLauncher/Update.xml";
            //数据保存数据库
            using (var db = new MyDbContext())
            {
                var url = db.Others.Where(x => x.ParamName == "自动升级URL").FirstOrDefault();
                if (null != url)
                {
                    urlSave = url.ParamValue.ToString();
                }
            }
            AutoUpdater.Start(urlSave);

            label_version.Text += Application.ProductVersion;

            string MyFileName = @"C:\games\preset_game.ini";
            if (MyFileName.Length < 1)
                return;
            string ShortName = MyFileName.Substring(MyFileName.LastIndexOf("\\") + 1);
            if (File.Exists(MyFileName))
            {
                //MessageBox.Show("文件：" + ShortName + "已经存在!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile(MyFileName);

                KeyDataCollection keyDatas = data["game list"];

                using (var db = new MyDbContext())
                {

                    foreach (var keyData in keyDatas)
                    {
                        Products products = new Products();
                        products.Key = keyData.Value;

                        /*
                        [game setting]
                        ; 默认游戏时长，5 分钟 = 300 秒
                        DEFAULT_GAMING_TIME = 300
                        ; 游戏可执行文件路径
                        GAME_EXECUTABLE_PATH = 002 - huozai\火灾事故.exe
                        ; 游戏名称，支持中文
                        GAME_NAME = 矿井安全 - 火灾
                        ; 游戏介绍影片、图片路径
                        INTRO_PATH = intro_guide\
                        WINDOW_NAME = "CoalMineVRProjectUE4  "
                        */

                        string MyFileNameInner = string.Format(@"C:\games\{0}\game_setting.ini", products.Key);
                        if (MyFileNameInner.Length < 1)
                            return;
                        string ShortNameInner = MyFileName.Substring(MyFileName.LastIndexOf("\\") + 1);
                        if (File.Exists(MyFileNameInner))
                        {
                            var parserInner = new FileIniDataParser();
                            IniData dataInner = parserInner.ReadFile(MyFileNameInner);

                            string DEFAULT_GAMING_TIME = dataInner["game setting"]["DEFAULT_GAMING_TIME"];
                            string GAME_EXECUTABLE_PATH = dataInner["game setting"]["GAME_EXECUTABLE_PATH"];
                            string GAME_NAME = dataInner["game setting"]["GAME_NAME"];
                            string INTRO_PATH = dataInner["game setting"]["INTRO_PATH"];
                            string WINDOW_NAME = dataInner["game setting"]["WINDOW_NAME"];

                            if (Path.IsPathRooted(GAME_EXECUTABLE_PATH))
                            {
                                products.ExePath = GAME_EXECUTABLE_PATH;
                            }
                            else
                            {
                                products.ExePath = string.Format(@"C:\games\{0}\{1}", products.Key, GAME_EXECUTABLE_PATH);
                            }
                            products.ExePath = GAME_EXECUTABLE_PATH;
                            products.ProductName = GAME_NAME;

                        }
                        else
                        {

                        }

                        db.Products.Add(products);
                    }
                    db.SaveChanges();
                }
                File.Delete(MyFileName);
            }
            else
            {
                //MessageBox.Show("文件：" + ShortName + "不存在!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            using (var db = new MyDbContext())
            {
                var products = db.Products;
                foreach (var product in products)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(510, 550);
                    //panel.BackColor = Color.Red;

                    Label label = new Label();
                    label.AutoSize = false;
                    label.Font = new Font("微软雅黑", 20, FontStyle.Regular, GraphicsUnit.Pixel);
                    label.TextAlign = ContentAlignment.TopCenter;
                    label.Text = product.ProductName;
                    label.Dock = DockStyle.Bottom;

                    panel.Controls.Add(label);

                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Tag = product;

                    pictureBox.Size = new Size(480, 480);
                    pictureBox.BackColor = Color.Black;

                    pictureBox.BackgroundImageLayout = ImageLayout.Zoom;

                    //判断文件夹是否已经存在
                    string MyFolderName = string.Format(@"C:\games\{0}\intro_guide", product.Key);
                    if (MyFolderName.Length < 1)
                        return;
                    string FolderName = MyFolderName.Substring(MyFolderName.LastIndexOf("\\") + 1);
                    if (Directory.Exists(MyFolderName))
                    {
                        //MessageBox.Show("文件夹：" + FolderName + "已经存在!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string[] Num1 = Directory.GetFiles(MyFolderName, "*.jpg");
                        string[] Num2 = Directory.GetFiles(MyFolderName, "*.png");
                        string[] MyFiles = Num1.Concat(Num2).ToArray();
                        if (MyFiles.Length > 0)
                        {
                            //Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                            //Image image = Image.FromFile(MyFiles[0]).GetThumbnailImage(480, 480, myCallback, IntPtr.Zero);

                            Image image = Image.FromFile(MyFiles[0]);
                            image = ScaleImage(image, 480, 480);
                            pictureBox.BackgroundImage = image;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("文件夹：" + FolderName + "不存在!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    pictureBox.Click += PictureBox_Click;
                    pictureBox.DoubleClick += PictureBox_DoubleClick;

                    panel.Controls.Add(pictureBox);

                    flowLayoutPanel1.Controls.Add(panel);

                }
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int nValue = ((Timer)sender).Tag == null ? 5 : ((Timer)sender).Tag.ToInt();
            nValue--;
            ((Timer)sender).Tag = nValue;

            if (nValue >= 0)
            {
                Console.WriteLine(nValue);

                int ScrollWidth = 0;//定义位置变量

                if (flowLayoutPanel1.HorizontalScroll.Value > 96)
                {
                    ScrollWidth = flowLayoutPanel1.HorizontalScroll.Value - 96;
                }
                else
                {
                    ScrollWidth = flowLayoutPanel1.HorizontalScroll.Minimum;
                }

                flowLayoutPanel1.AutoScrollPosition = new Point(ScrollWidth, 0);
            }
            else
            {
                ((Timer)sender).Stop();
            }


        }

        private void button_next_Click(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick1;
            timer.Start();
        }

        private void Timer_Tick1(object sender, EventArgs e)
        {
            int nValue = ((Timer)sender).Tag == null ? 5 : ((Timer)sender).Tag.ToInt();
            nValue--;
            ((Timer)sender).Tag = nValue;

            if (nValue > 0)
            {
                int ScrollWidth = 0;//定义位置变量

                if (flowLayoutPanel1.HorizontalScroll.Value + 96 > flowLayoutPanel1.HorizontalScroll.Maximum)
                {
                    ScrollWidth = flowLayoutPanel1.HorizontalScroll.Maximum;
                }
                else
                {
                    ScrollWidth = flowLayoutPanel1.HorizontalScroll.Value + 96;
                }

                flowLayoutPanel1.AutoScrollPosition = new Point(ScrollWidth, 0);
            }
            else
            {
                ((Timer)sender).Stop();
            }
        }

        public const int KEYEVENTF_KEYUP = 2;

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);


        [DllImport("user32.dll")]
        private static extern void LockWorkStation();


        private void button_lock_Click(object sender, EventArgs e)
        {
            LockWorkStation();
        }

        int nIndex = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (null != pictureBox)
            {
                Products products = (Products)pictureBox.Tag;

                //判断文件夹是否已经存在
                string MyFolderName = string.Format(@"C:\games\{0}\intro_guide", products.Key);
                if (MyFolderName.Length < 1)
                    return;
                string FolderName = MyFolderName.Substring(MyFolderName.LastIndexOf("\\") + 1);
                if (Directory.Exists(MyFolderName))
                {
                    //MessageBox.Show("文件夹：" + FolderName + "已经存在!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string[] Num1 = Directory.GetFiles(MyFolderName, "*.jpg");
                    string[] Num2 = Directory.GetFiles(MyFolderName, "*.png");
                    string[] MyFiles = Num1.Concat(Num2).ToArray();
                    if (MyFiles.Length > 0)
                    {
                        Image image = Image.FromFile(MyFiles[nIndex]);
                        pictureBox.BackgroundImage = image;
                        nIndex++;

                        if (nIndex >= MyFiles.Length)
                        {
                            nIndex = 0;
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("文件夹：" + FolderName + "不存在!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 按指定宽高缩放图片
        /// </summary>
        /// <param name="image">原图片</param>
        /// <param name="dstWidth">目标图片宽</param>
        /// <param name="dstHeight">目标图片高</param>
        /// <returns></returns>
        public static Image ScaleImage(Image image, int dstWidth, int dstHeight)
        {
            Graphics g = null;
            try
            {
                //按比例缩放           
                float scaleRate = getWidthAndHeight(image.Width, image.Height, dstWidth, dstHeight);
                int width = (int)(image.Width * scaleRate);
                int height = (int)(image.Height * scaleRate);

                //将宽度调整为4的整数倍
                if (width % 4 != 0)
                {
                    width = width - width % 4;
                }

                Bitmap destBitmap = new Bitmap(width, height);
                g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);

                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, new Rectangle((width - width) / 2, (height - height) / 2, width, height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                return destBitmap;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
            }

            return null;
        }

        /// <summary>
        /// 获取图片缩放比例
        /// </summary>
        /// <param name="oldWidth">原图片宽</param>
        /// <param name="oldHeigt">原图片高</param>
        /// <param name="newWidth">目标图片宽</param>
        /// <param name="newHeight">目标图片高</param>
        /// <returns></returns>
        public static float getWidthAndHeight(int oldWidth, int oldHeigt, int newWidth, int newHeight)
        {
            //按比例缩放           
            float scaleRate = 0.0f;
            if (oldWidth >= newWidth && oldHeigt >= newHeight)
            {
                int widthDis = oldWidth - newWidth;
                int heightDis = oldHeigt - newHeight;
                if (widthDis > heightDis)
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
                else
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
            }
            else if (oldWidth >= newWidth && oldHeigt < newHeight)
            {
                scaleRate = newWidth * 1f / oldWidth;
            }
            else if (oldWidth < newWidth && oldHeigt >= newHeight)
            {
                scaleRate = newHeight * 1f / oldHeigt;
            }
            else
            {
                int widthDis = newWidth - oldWidth;
                int heightDis = newHeight - oldHeigt;
                if (widthDis > heightDis)
                {
                    scaleRate = newHeight * 1f / oldHeigt;
                }
                else
                {
                    scaleRate = newWidth * 1f / oldWidth;
                }
            }
            return scaleRate;
        }
    }
}
