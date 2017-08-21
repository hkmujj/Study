using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting.TrainInfo
{
    public interface IMonitorDataViewModel : IClearData, INotifyPropertyChanged
    {
        string CirculationNum { get; set; }
        string ErrorFlag { get; set; }
        string TrainID { get; set; }
        string UseCarID { get; set; }
        string DriverID { get; set; }
        string FitDriverID { get; set; }
        string Speed { get; set; }
        string KilometerPost { get; set; }
        string WorkingCondition { get; set; }
        string TotallNum { get; set; }
        string TotalWeight { get; set; }
        string RememberLong { get; set; }
        string PassengerComplement { get; set; }
        string InstallationStatus { get; set; }
    }
}