using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Engine.TCMS.Turkmenistan.Apps
{
    /// <summary>
    /// StartEntry.xaml 的交互逻辑
    /// </summary>
    public partial class StartEntry : Application
    {
        /// <summary>
        /// 引发 <see cref="E:System.Windows.Application.Startup"/> 事件。
        /// </summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.Windows.StartupEventArgs"/>。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var boot = new TurkmenistanBootStraper();
            boot.Run();
        }
    }
}
