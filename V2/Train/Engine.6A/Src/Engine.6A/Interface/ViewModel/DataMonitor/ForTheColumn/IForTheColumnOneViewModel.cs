using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.DataMonitor.ForTheColumn
{
    public interface IForTheColumnOneViewModel : IClearData, INotifyPropertyChanged
    {
        string LeakageCurrentOneWay { get; set; }
        string InputVoltageOneWay { get; set; }
        string InputCurrentOneWay { get; set; }
        string SupplyVoltageOneWay { get; set; }
        string SupplyCurrentOneWay { get; set; }
        string HalfVoltageOneWay { get; set; }
        string UsePowerOneWay { get; set; }
        string LeakageCurrentTwoWay { get; set; }
        string InputVoltageTwoWay { get; set; }
        string InputCurrentTwoWay { get; set; }
        string SupplyVoltageTwoWay { get; set; }
        string SupplyCurrentTwoWay { get; set; }
        string HalfVoltageTwoWay { get; set; }
        string UsePowerTwoWay { get; set; }

    }
}