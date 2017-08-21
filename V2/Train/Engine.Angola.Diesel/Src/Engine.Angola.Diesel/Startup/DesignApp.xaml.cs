using System;
using System.Windows;
using CommonUtil.Util;

namespace Engine.Angola.Diesel.Startup
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class DesignApp : Application
    {
        /// <summary>引发 <see cref="E:System.Windows.Application.Startup" /> 事件。</summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.StartupEventArgs" /> 。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var bootstrapper = new SubwayAngolaDieselMefBootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private void HandleException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            LogMgr.Error(string.Format("Un handle exception : {0}", ex));
            //ExceptionPolicy.HandleException(ex, "Default Policy");
            //MessageBox.Show(StockTraderRI.Properties.Resources.UnhandledException);
            //Environment.Exit(1);
        }
    }
}
