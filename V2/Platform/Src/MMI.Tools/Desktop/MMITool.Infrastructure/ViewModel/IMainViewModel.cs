using System.Windows.Input;

namespace MMITool.Infrastructure.ViewModel
{
    /// <summary>
    /// 主窗口的
    /// </summary>
    public interface IMainViewModel : MMI.Facility.WPFInfrastructure.ViewModel.IMainViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        ICommand HelpCommand { set; get; }
    }
}