using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo
{
    public interface IOlLevelMeterTestViewModel : IClearData, INotifyPropertyChanged
    {
        string OilMass { get; set; }
        string CirculationNum { get; set; }
        string ErrorFlag { get; set; }
    }
}