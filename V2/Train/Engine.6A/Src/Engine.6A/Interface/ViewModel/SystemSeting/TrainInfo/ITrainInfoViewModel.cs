using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo
{
    public interface ITrainInfoViewModel : IClearData, INotifyPropertyChanged
    {
        IMonitorDataViewModel MonitorData { get; }
        IMicrocomputerInfoViewModel MicrocomputerInfo { get; }
        IElectropsychometerTestViewModel ElectropsychometerTest { get; }
        IOlLevelMeterTestViewModel OlLevelMeterTest { get; }
        ObservableCollection<string> ViewCollection { get; set; }
    }
}