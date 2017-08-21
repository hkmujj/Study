using System;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using General.FlashLoader.FlashView.Model;

namespace General.FlashLoader.FlashView
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppLog.Info("flash start arg != null ? {0}", args != null);

            if (args != null && args.Any())
            {
                AppLog.Info("flash start arg count = {0}", args.Length);
                AppLog.Info("Flash start arg content = \r\n{0}", args[0]);

                var startArg = DataSerialization.DeserializeFromXmlFile<FlashStartArg>(args[0].Trim('\"'));
                Application.Run(new MainForm(startArg));
            }
        }
    }
}
