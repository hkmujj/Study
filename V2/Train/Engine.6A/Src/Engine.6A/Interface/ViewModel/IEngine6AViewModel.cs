using System.Windows;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.Interface.ViewModel.SystemSeting;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.Interface.ViewModel
{
    public interface IEngine6AViewModel : IViewModelBase
    {
        Visibility MMIBlack { get; set; }
        IMainViewModel MainView { get; }
        IDialViewModel Dial { get; }
        IStartingViewModel Starting { get; }
        ITitleViewModel Title { get; }
        IDataMonitorViewModel DataMonitor { get; }
        IVideoViewModel Video { get; }
        IFaultViewModel Fault { get; }
        ISystemSettingViewModel SystemSetting { get; }
        IButtonViewModel Button { get; }
    }
}