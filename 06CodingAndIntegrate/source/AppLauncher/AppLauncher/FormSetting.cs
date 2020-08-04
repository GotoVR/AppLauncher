using AppLauncher.Data;
using AppLauncher.Models;
using AppLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AppLauncher
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            FormTips formTips = new FormTips();
            formTips.Title = "提示";
            formTips.Msg = "确定要重置应用程序设置吗？";
            formTips.Pic = Resources.警告;
            DialogResult dialogResult = formTips.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {

            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            //模拟除零错误
            //int a = 1;
            //int b = 0;
            //int c = a / b; 

            //修改SQLite数据库密码
            //DbOperator db = new DbOperator();
            //db.ChangePassword("");

            using (var db = new MyDbContext())
            {
                var result1 = db.Products.RemoveRange(db.Products);
                var result3 = db.ProductsDetails.RemoveRange(db.ProductsDetails);
                var result2 = db.Users.RemoveRange(db.Users);
                var result4 = db.Others.RemoveRange(db.Others);
                //var result4 = db.MeasureDatas.RemoveRange(db.MeasureDatas);

                int count = db.SaveChanges();
            }

            List<Products> products = new List<Products>()
            {
                
            };

            //List<MeasureData> measureDatas = new List<MeasureData>()
            //{
            //    new MeasureData() { XueYang_baoHeDu = "", XueYang_guanZhuZhiShu = "", XueYang_maiBo = "", CheckDate = DateTime.Now },
            //};

            //string[] coms = SerialPort.GetPortNames();

            //List<HidDevice> hidDevices = HidSharp.DeviceList.Local.GetHidDevices().ToList();

            //List<CommPort> commPorts = new List<CommPort>()
            //{
            //    new CommPort() { Weight = 1, PortName = "血压", PortNum = coms.Length > 0 ? coms[0] : "", IsUse = true },
            //    new CommPort() { Weight = 1, PortName = "体温", PortNum = coms.Length > 0 ? coms[0] : "", IsUse = true },
            //    new CommPort() { Weight = 1, PortName = "心电", PortNum = coms.Length > 0 ? coms[0] : "", IsUse = true },
            //    new CommPort() { Weight = 1, PortName = "血氧", PortNum = coms.Length > 0 ? coms[0] : "", IsUse = true },
            //    new CommPort() { Weight = 1, PortName = "人体成份", PortNum = coms.Length > 0 ? hidDevices[0].VendorID.ToString() + "||" + hidDevices[0].ProductID.ToString() : "", IsUse = true },
            //    new CommPort() { Weight = 1, PortName = "身高体重", PortNum = coms.Length > 0 ? coms[0] : "", IsUse = true },
            //};

            List<Others> others = new List<Others>()
            {
                //    new Other() { ParamName = "体检结果上传", ParamValue = "http://10.168.1.209:8085/healthMachine/save"},
                //    new Other() { ParamName = "会员认证URL", ParamValue = "http://10.168.1.209:8085/healthMachine/getUser"},
                //    new Other() { ParamName = "获取时间URL", ParamValue = "http://10.168.1.209:8085/healthMachine/updateUser"},
                //    new Other() { ParamName = "设备注册URL", ParamValue = "http://10.168.1.209:8085/healthMachine/saveMachine"},
                
                new Others() { ParamName = "自动升级URL", ParamValue = "http://applauncher.cuangeju.cn/AppLauncher/Update.xml"},

                //    new Other() { ParamName = "打印标题", ParamValue = "体检报告"},
                //    new Other() { ParamName = "欢迎内容", ParamValue = "欢迎使用健康小站(welcomeText)"},
                //    new Other() { ParamName = "脚注", ParamValue = "【温馨提示】坚持适量体力活动；合理膳食，适当限制钠盐及脂肪摄入，增加蔬菜与水果摄入；节制饮酒；不吸烟；保持正常体重，超重和肥胖者应减轻体重；保持心理平衡，对工作与生活保持良好的心态。"},
                //    new Other() { ParamName = "收缩压范围", ParamValue = "90|140|160|180"},
                //    new Other() { ParamName = "舒张压范围", ParamValue = "60|90|100|110"},
                //    new Other() { ParamName = "脉搏范围", ParamValue = "60|100"},
                //    new Other() { ParamName = "脂肪率男", ParamValue = "10|19.9"},
                //    new Other() { ParamName = "脂肪率女", ParamValue = "20|29.9"},
                //    new Other() { ParamName = "含水量男", ParamValue = "50.1|65"},
                //    new Other() { ParamName = "含水量女", ParamValue = "45.1|60"},
                //    new Other() { ParamName = "BMI", ParamValue = "18.5|23.9|27.9"},
                //    new Other() { ParamName = "摄像头分辨率", ParamValue = "Width:1920,Height:896"},
                //    new Other() { ParamName = "体温范围", ParamValue = "35.9|37.1"},
            };

            using (var db = new MyDbContext())
            {
                db.Products.AddRange(products);
                db.Others.AddRange(others);

                //db.MeasureDatas.AddRange(measureDatas);
                //db.CommPorts.AddRange(commPorts);


                int count = db.SaveChanges();
            }

            FormTips formTips1 = new FormTips();
            formTips1.Title = "提示";
            formTips1.Msg = "重置已完成，将要重启应用程序！";
            formTips1.Pic = Resources.警告;
            DialogResult dialogResult1 = formTips1.ShowDialog();
            if (dialogResult1 == DialogResult.OK)
            {
                Application.Restart();
            }
            else if (dialogResult1 == DialogResult.Cancel)
            {
                //Application.Restart();
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (textBox_password.Text == "655445")
            {
                FormSettingDetail formSettingDetail = new FormSettingDetail();
                formSettingDetail.ShowDialog();

                this.Close();
            }
        }

        private void button_vol_plus_Click(object sender, EventArgs e)
        {
            //加音量 
            SendMessage(this.Handle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_UP * 0x10000);
        }

        private void button_vol_minus_Click(object sender, EventArgs e)
        {
            //减音量 
            SendMessage(this.Handle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_DOWN * 0x10000);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);
        const uint WM_APPCOMMAND = 0x319;
        const uint APPCOMMAND_VOLUME_UP = 0x0a;
        const uint APPCOMMAND_VOLUME_DOWN = 0x09;
        const uint APPCOMMAND_VOLUME_MUTE = 0x08;

        private void button1_Click(object sender, EventArgs e)
        {
            //加音量 
            SendMessage(this.Handle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_UP * 0x10000);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //减音量 
            SendMessage(this.Handle, WM_APPCOMMAND, 0x30292, APPCOMMAND_VOLUME_DOWN * 0x10000);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //静音 
            SendMessage(this.Handle, WM_APPCOMMAND, 0x200eb0, APPCOMMAND_VOLUME_MUTE * 0x10000);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            FormTips formTips = new FormTips();
            formTips.Title = "确定重启电脑吗？";
            formTips.Msg = "确定重启电脑吗？";
            formTips.Pic = Resources.警告;
            formTips.ShowDialog();
        }
    }
}
