using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo
{
    public interface IMicrocomputerInfoViewModel : IClearData, INotifyPropertyChanged
    {
        string CirculationNum { get; set; }
        string ErrorFlag { get; set; }
        string OccupiedEnd { get; set; }
        string HandleLevel { get; set; }
        string Pantograph { get; set; }
        string MainFaultStatus { get; set; }
        string BigFloodgate { get; set; }
        string SmallFloodgate { get; set; }
        string ParkingCut { get; set; }
    }
}