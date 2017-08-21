using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MMITool.Common.Util;

namespace MMITool.Platform
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //InitGlobalization();

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            var si = AppDomain.CurrentDomain.SetupInformation;

            var exe = Path.Combine(si.ApplicationBase, si.ApplicationName);

            try
            {

                var ico = Icon.ExtractAssociatedIcon(exe);

                MainWindow.Icon = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception exception)
            {
                LogMgr.Error(string.Format("Can not found .ico from {0}, {1}", exe, exception));
            }

            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        //protected override void OnLoadCompleted(NavigationEventArgs e)
        //{
        //    base.OnLoadCompleted(e);
        //    InitGlobalization();
        //}

        private void InitGlobalization()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");

            //var res =
            //    (ResourceDictionary)
            //        Application.LoadComponent(new Uri(string.Format("/MMITool.Resource;component/resource.{0}.xaml", Thread.CurrentThread.CurrentCulture.Name),
            //            System.UriKind.Relative));

            //Application.Current.Resources.MergedDictionaries.Add(res);
        }

        private void RunInDebugMode()
        {
            var bootstrapper = new MMIToolBootstrapper();
            bootstrapper.Run();
        }

        private void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                var bootstrapper = new MMIToolBootstrapper();
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
                return;

            LogMgr.Error(string.Format("Un handle exception : {0}", ex));
            //ExceptionPolicy.HandleException(ex, "Default Policy");
            //MessageBox.Show(StockTraderRI.Properties.Resources.UnhandledException);
            //Environment.Exit(1);
        }
    }
}
