using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.DataMonitor
{
    /// <summary>
    /// 绝缘
    /// </summary>
    public interface IInsulationViewModel : IClearData, INotifyPropertyChanged
    {
        double TestVoltage { get; set; }
        string TestType { get; set; }
        string TestResult { get; set; }
        string ElectricKey { get; set; }
        string ElectricNetwork { get; set; }
        string PowerModule { get; set; }
        string AlarmThreshold { get; set; }
    }
}