﻿using System.Collections.Generic;
using System.Windows;

namespace Other.ContactLine.JW4G
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 引发 <see cref="E:System.Windows.Application.Startup"/> 事件。
        /// </summary>
        /// <param name="e">一个包含事件数据的 <see cref="T:System.Windows.StartupEventArgs"/>。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var lis=new List<string>();
            lis.Add("sss");
            var boot = new ContractLineJW4GMefBootstrapper();
            boot.Run();
        }
    }
}