using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.DataMonitor
{
    public interface ISleepViewModel : IClearData, INotifyPropertyChanged
    {
        string OneDriverOpen { get; set; }
        string TwoDriverOpen { get; set; }
        string OneVideoCheck { get; set; }
        string TwoVideoCheck { get; set; }
        string BoardInfo { get; set; }
        string DriverState { get; set; }
    }
}