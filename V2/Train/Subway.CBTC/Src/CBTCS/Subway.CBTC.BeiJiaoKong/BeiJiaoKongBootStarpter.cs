using System;
using System.IO;
using System.Windows;
using MMI.Facility.WPFInfrastructure;
using Subway.CBTC.BeiJiaoKong.Models;
using Subway.CBTC.BeiJiaoKong.Views.App;

namespace Subway.CBTC.BeiJiaoKong
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    public class BeiJiaoKongBootStarpter : MMIMefBootstrapper
    {
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
            GlobalParams.Instance.Initalize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "Subway.CBTC.BeiJiaoKong", "Config"), true);

            //GlobalParams.Instance.Initialize(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Subway.CBTC.BeiJiaoKong//Config"));
            return new MainWindow();
        }

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
                return;
            }
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.MainWindow = win;
            Application.Current.MainWindow.Show();
        }
    }
}