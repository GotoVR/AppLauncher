using HZH_Controls.Forms;
using AppLauncher.Models;
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
    public partial class FormTips : FrmWithOKCancel1
    {
        private Form form;
        private Users users;
        public FormTips()
        {
            InitializeComponent();
        }

        public Form FormToClose { get => form; set => form = value; }
        public Users User { get => users; set => users = value; }

        protected override void DoEnter()
        {
            switch (this.Msg)
            {
                case "确定重启电脑吗？":
                    {
                        //重启
                        System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                        myProcess.StartInfo.FileName = "cmd.exe";//启动cmd命令
                        myProcess.StartInfo.UseShellExecute = false;//是否使用系统外壳程序启动进程
                        myProcess.StartInfo.RedirectStandardInput = true;//是否从流中读取
                        myProcess.StartInfo.RedirectStandardOutput = true;//是否写入流
                        myProcess.StartInfo.RedirectStandardError = true;//是否将错误信息写入流
                        myProcess.StartInfo.CreateNoWindow = true;//是否在新窗口中启动进程
                        myProcess.Start();//启动进程
                        myProcess.StandardInput.WriteLine("shutdown -r -t 0");//执行重启计算机命令
                        break;
                    }
                case "恭喜您，注册成功！\r\n即将进入测量~":
                    {
                        if (null != FormToClose)
                        {
                            FormToClose.Close();
                        }

                        //FormMeasure formMeasure = new FormMeasure();
                        //formMeasure.User = user;
                        //formMeasure.Show();

                        break;
                    }
                case "没有打印数据！":
                    {
                        if (null != FormToClose)
                        {
                            FormToClose.Close();
                        }

                        break;
                    }
                case "确认不打印直接退出吗？":
                    {
                        if (null != FormToClose)
                        {
                            FormToClose.Close();
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            base.DoEnter();
        }

        protected override void DoEsc()
        {
            switch (this.Msg)
            {
                case "确定重启电脑吗？":
                    {
                        break;
                    }
                case "恭喜您，注册成功！\r\n即将进入测量~":
                    {
                        if (null != FormToClose)
                        {
                            FormToClose.Close();
                        }

                        break;
                    }
                case "确认不打印直接退出吗？":
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            base.DoEsc();

        }

        private void ucBtnExt1_BtnClick(object sender, EventArgs e)
        {
            DoEnter();
        }
    }
}
