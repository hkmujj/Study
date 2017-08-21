using System.Windows;
using MMI.Facility.WPFInfrastructure.ViewModel;

namespace MMI.Facility.WPFInfrastructure.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// 根窗口
        /// </summary>
        Window ShellWindow { get; }

        /// <summary>
        /// 主窗口 viewmodel
        /// </summary>
        IMainViewModel MainViewModel { get; }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        string ConfigPath { get; }

        /// <summary>
        /// 程序所在目录
        /// </summary>
        string AppPath { get; }

        /// <summary>
        /// addin 目录
        /// </summary>
        string AddinPath { get; }

        /// <summary>
        /// addin/config/ 
        /// </summary>
        string AddinConfigPath { get; }
    }
}
