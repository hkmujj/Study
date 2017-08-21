using System.Collections.ObjectModel;
using System.ComponentModel;
using Engine._6A.Interface.ViewModel.DataMonitor.ForTheColumn;

namespace Engine._6A.Interface.ViewModel.DataMonitor
{
    public interface IDataMonitorViewModel : IClearData, INotifyPropertyChanged
    {
        IBrakeViewModel Brake { get; }
        IFirePreventionViewModel FirePrevention { get; }
        IInsulationViewModel Insulation { get; }
        IRunLineOneViewModelBase RunLineOne { get; }
        IForTheColumnOneViewModel ForTheColumnOne { get; }
        IForTheColumnTwoViewModel ForTheColumnTwo { get; }
        IRunLineTwoViewModel RunLineTwo { get; }
        ISleepViewModel Sleep { get; }
        ObservableCollection<string> ColumnCollection { get; set; }
    }
}