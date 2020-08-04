using AForge.Video.DirectShow;
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
    public partial class FormFace : Form
    {
        /// <summary>
        /// 视频输入设备信息
        /// </summary>
        private FilterInfoCollection filterInfoCollection;

        /// <summary>
        /// RGB 摄像头索引
        /// </summary>
        private int rgbCameraIndex = 0;

        /// <summary>
        /// RGB摄像头设备
        /// </summary>
        private VideoCaptureDevice rgbDeviceVideo;

        public FormFace()
        {
            InitializeComponent();

            InitializeVideo();
        }

        private void FormFace_Load(object sender, EventArgs e)
        {
            pictureBox1.Parent = videoSourcePlayer1;
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

        private void InitializeVideo()
        {
            //在点击开始的时候再坐下初始化检测，防止程序启动时有摄像头，在点击摄像头按钮之前将摄像头拔掉的情况
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //必须保证有可用摄像头
            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("未检测到摄像头，请确保已安装摄像头或驱动!");
                return;
            }
            if (videoSourcePlayer1.IsRunning)
            {
                //关闭摄像头
                if (videoSourcePlayer1.IsRunning)
                {
                    videoSourcePlayer1.SignalToStop();
                    videoSourcePlayer1.Hide();
                }
            }
            else
            {
                //获取filterInfoCollection的总数
                int maxCameraCount = filterInfoCollection.Count;

                //仅打开RGB摄像头，IR摄像头控件隐藏
                rgbDeviceVideo = new VideoCaptureDevice(filterInfoCollection[rgbCameraIndex <= maxCameraCount ? rgbCameraIndex : 0].MonikerString);

                VideoCapabilities videoCapabilities = rgbDeviceVideo.VideoCapabilities[14];

                //读取设置的摄像头分辨率
                //using (var db = new MyDbContext())
                //{
                //    var other = db.Others.Where(x => x.ParamName == "摄像头分辨率").FirstOrDefault();

                //    if (null != other)
                //    {
                //        string value1 = other.ParamValue;

                //        foreach (var vc in rgbDeviceVideo.VideoCapabilities)
                //        {
                //            string value = string.Format("Width:{0},Height:{1}", vc.FrameSize.Width, vc.FrameSize.Height);
                //            if (value == value1)
                //            {
                //                videoCapabilities = vc;
                //                break;
                //            }
                //        }
                //    }
                //}

                panel1.Size = new Size(videoCapabilities.FrameSize.Width / 2, videoCapabilities.FrameSize.Height / 2);
                //rgbVideoSource.Size = new Size(videoCapabilities.FrameSize.Width / 4, videoCapabilities.FrameSize.Height / 4);
                //rgbVideoSource.Size = new Size(videoCapabilities.FrameSize.Width / 2, videoCapabilities.FrameSize.Height / 2);
                //rgbVideoSource.Location = new Point((panel1.Width - rgbVideoSource.Size.Width) / 2, (panel1.Height - rgbVideoSource.Size.Height) / 2);

                rgbDeviceVideo.VideoResolution = videoCapabilities;
                videoSourcePlayer1.VideoSource = rgbDeviceVideo;
                videoSourcePlayer1.Start();
            }
        }
    }
}
