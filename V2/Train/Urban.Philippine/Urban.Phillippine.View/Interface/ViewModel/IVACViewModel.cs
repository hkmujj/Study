using System.Collections.ObjectModel;
using System.Windows.Input;
using Urban.Phillippine.View.Config;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IVACViewModel
    {
        ObservableCollection<VACViewUnit> VAC { get; set; }
        double CurrentTemperature { get; set; }
        ICommand SetTemperrature { get; }
        ICommand SetModel { get; }
        ICommand ChangedVACPage { get; }
    }
}