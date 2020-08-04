using AppLauncher.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vlc.DotNet.Forms;
using WinFormAnimation;
using Path = WinFormAnimation.Path;

namespace AppLauncher
{
    public partial class FormVideo : Form
    {
        private readonly Animator2D _animator = new Animator2D();

        public FormVideo()
        {
            InitializeComponent();

            _animator.Paths = new Path2D[]{ new Path2D(
                new Path(0, 0, 2000, AnimationFunctions.CubicEaseOut),
                new Path(0, 300, 2000, AnimationFunctions.CubicEaseIn)) };
        }

        private void VlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;

            if (currentDirectory == null)
                return;
            if (IntPtr.Size == 4)
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.GetFullPath(@".\libvlc\win-x86\"));
            else
                e.VlcLibDirectory = new DirectoryInfo(System.IO.Path.GetFullPath(@".\libvlc\win-x64\"));

            if (!e.VlcLibDirectory.Exists)
            {
                var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
                {
                    Description = "Select Vlc libraries folder.",
                    RootFolder = Environment.SpecialFolder.Desktop,
                    ShowNewFolderButton = true
                };
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    e.VlcLibDirectory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                }
            }
        }

        System.IO.Stream stream1;

        void PlayVideo1()
        {
            string[] mediaOptions = new string[] { "input-repeat=10000" }; //x为循环次数

            try
            {
                string strFilePath = Environment.CurrentDirectory + "\\Resources\\introvid_xueya.mp4";
                stream1 = new FileStream(strFilePath, FileMode.Open);
            }
            catch (Exception)
            {
                stream1 = new MemoryStream(Resources.introvid_xueya);
            }



            vlcControl1.SetMedia(stream1, mediaOptions);//本地视频
            //vlcControl1.SetMedia(new System.IO.FileInfo(url1));//本地视频
            vlcControl1.VlcMediaPlayer.Play();

            vlcControl1.VlcMediaPlayer.Video.FullScreen = false;
            vlcControl1.VlcMediaPlayer.Video.AspectRatio = "16:9";

            //dosomething();

            vlcControl1.MouseClick += VlcControl1_MouseClick;

            //vlcControl1.VlcMediaPlayer.Time = Start;
            //vlcControl1.VlcMediaPlayer.TimeChanged += VlcMediaPlayer_TimeChanged1;
            //vlcControl1.VlcMediaPlayer.EndReached += VlcMediaPlayer_EndReached1;
        }

        private void VlcControl1_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FormVideo_Load(object sender, EventArgs e)
        {
            panel2.Parent = vlcControl1;
            //panel2.Location = new Point(0, 668);

            Thread t1 = new Thread(PlayVideo1);
            t1.Start();
        }

        private void vlcControl1_MouseClick(object sender, MouseEventArgs e)
        {
            _animator.Play(panel2, Animator2D.KnownProperties.Location);
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
