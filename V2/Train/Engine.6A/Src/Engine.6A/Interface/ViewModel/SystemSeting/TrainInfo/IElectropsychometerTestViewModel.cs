using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo
{
    public interface IElectropsychometerTestViewModel : IClearData, INotifyPropertyChanged
    {
        string CirculationNum { get; set; }
        string ErrorFlag { get; set; }
        string PositiveActivePower { get; set; }
        string ReverseActivePower { get; set; }
        string PositiveReactivePower { get; set; }
        string ReverseReactivePower { get; set; }
        string Voltage { get; set; }
        string Current { get; set; }
        string VoltageFrequency { get; set; }
        string PowerFactor { get; set; }
        string ActivePower { get; set; }
        string ReactivePower { get; set; }
    }
}