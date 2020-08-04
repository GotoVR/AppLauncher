using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLauncher
{
    static class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("Logging");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool bExist;
            Mutex MyMutex = new Mutex(true, "AppLauncher", out bExist);
            if (bExist)
            {
                log.Info("应用程序启动");

                Application.Run(new FormMain());
                MyMutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("程序已经运行！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Application.Run(new FormMain());
        }
    }
}
