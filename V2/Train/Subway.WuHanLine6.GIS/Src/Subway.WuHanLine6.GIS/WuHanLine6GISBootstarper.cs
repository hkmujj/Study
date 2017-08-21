using System;
using System.IO;
using System.Windows;
using CommonUtil.Util;
using MMI.Facility.WPFInfrastructure;
using Subway.WuHanLine6.GIS.Models;
using Subway.WuHanLine6.GIS.Views.App;

namespace Subway.WuHanLine6.GIS
{
    public class WuHanLine6GISBootstarper : MMIMefBootstrapper
    {
        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implementation ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            var win = Shell as Window;
            if (win == null)
            {
                AppLog.Error("初始化窗口失败！");
                AppLog.Error($"{nameof(Shell)} 不能为空！或者Shell类型不是Windows类型！");
                return;
            }
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = win;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject"/>, the
        ///             <see cref="T:Microsoft.Practices.Prism.Bootstrapper"/> will attach the default <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> of
        ///             the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached property
        ///             in order to be able to add regions by using the <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/>
        ///             attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            GlobalParams.Instance.Initlization(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Subway.WuHanLine6.GIS.Left", "Config"));
            return new GISWindows();
        }
    }
}