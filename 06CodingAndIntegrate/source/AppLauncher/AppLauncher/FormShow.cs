using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLauncher
{
    public partial class FormShow : Form
    {
        public FormShow()
        {
            InitializeComponent();

            appIdleAction = new Action<object, EventArgs>(Application_Idle);
            appIdleEvent = new EventHandler(appIdleAction);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (this.AppProcess == null || this.AppProcess.HasExited)
            {
                this.AppProcess = null;
                Application.Idle -= appIdleEvent;
                return;
            }
            if (AppProcess.MainWindowHandle == IntPtr.Zero)
            { 
                return;
            }
            //Application.Idle -= appIdleEvent;
            if (EmbedProcess(AppProcess, panel1))
            {
                Application.Idle -= appIdleEvent;
            }

            //ShowWindow(AppProcess.MainWindowHandle, SW_SHOWNORMAL);

            //var parent = GetParent(AppProcess.MainWindowHandle);//你妹，不管用，全是0
            //if (parent == this.Handle)
            //{
            //    Application.Idle -= appIdleEvent;
            //}
        }

        private bool EmbedProcess(Process app, Control control)
        {
            // Get the main handle
            if (app == null || app.MainWindowHandle == IntPtr.Zero || control == null) return false;

            embedResult = 0;

            try
            {
                // Put it into this container
                embedResult = Win32API.SetParent(app.MainWindowHandle, control.Handle);
            }
            catch (Exception)
            { }
            try
            {
                // Remove border and whatnot               
                Win32API.SetWindowLong(new HandleRef(this, app.MainWindowHandle), Win32API.GWL_STYLE, Win32API.WS_VISIBLE);
            }
            catch (Exception)
            { }
            try
            {
                // Move the window to overlay it on this window
                Win32API.MoveWindow(app.MainWindowHandle, 0, 0, control.Width, control.Height, true);
            }
            catch (Exception)
            { }

            if (ShowEmbedResult)
            {
                var errorString = Win32API.GetLastError();
                MessageBox.Show(errorString);
            }

            return (embedResult != 0);
        }

        private string m_AppFilename = "";
        public string AppFilename
        {
            get
            {
                return m_AppFilename;
            }
            set
            {
                if (value == null || value == m_AppFilename) return;
                var self = Application.ExecutablePath;
                if (value.ToLower() == self.ToLower())
                {
                    MessageBox.Show("Please don't embed yourself！", "SmileWei.EmbeddedApp");
                    return;
                }
                if (!value.ToLower().EndsWith(".exe"))
                {
                    MessageBox.Show("target is not an *.exe！", "SmileWei.EmbeddedApp");
                }
                if (!File.Exists(value))
                {
                    MessageBox.Show("target does not exist！", "SmileWei.EmbeddedApp");
                    return;
                }
                m_AppFilename = value;
            }
        }

        public Process AppProcess { get; set; }
        public bool IsStarted { get { return (this.AppProcess != null); } }

        public bool ShowEmbedResult { get; set; }

        private void FormShow_Load(object sender, EventArgs e)
        {
            Start();
        }

        Action<object, EventArgs> appIdleAction = null;
        EventHandler appIdleEvent = null;

        public void Start()
        {
            if (AppProcess != null)
            {
                Stop();
            }

            try
            {
                ProcessStartInfo info = new ProcessStartInfo(this.m_AppFilename);

                //设置启动动作,确保以管理员身份运行
                info.Verb = "open";

                info.UseShellExecute = false;
                info.WindowStyle = ProcessWindowStyle.Minimized;
                //info.WindowStyle = ProcessWindowStyle.Hidden;
                AppProcess = System.Diagnostics.Process.Start(info);
                // Wait for process to be created and enter idle condition
                AppProcess.WaitForInputIdle();
                //todo:下面这两句会引发 NullReferenceException 异常，不知道怎么回事                
                //AppProcess.Exited += new EventHandler(AppProcess_Exited);
                //AppProcess.EnableRaisingEvents = true;
                Application.Idle += appIdleEvent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("{1}{0}{2}{0}{3}"
                    , Environment.NewLine
                    , "*" + ex.ToString()
                    , "*StackTrace:" + ex.StackTrace
                    , "*Source:" + ex.Source
                    ), "Failed to load app.");
                if (AppProcess != null)
                {
                    if (!AppProcess.HasExited)
                    {
                        AppProcess.Kill();
                    }
                    AppProcess = null;
                }
            }

        }

        public int embedResult = 0;

        public void Stop()
        {
            if (AppProcess != null)// && AppProcess.MainWindowHandle != IntPtr.Zero)
            {
                try
                {
                    if (!AppProcess.HasExited)
                    {
                        AppProcess.Kill();
                    }
                }
                catch (Exception)
                {
                }
                AppProcess = null;
                embedResult = 0;
            }
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            Stop();
            this.Close();
        }
    }
}
