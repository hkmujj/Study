using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting
{
    public interface IVersionInfoViewModel : IClearData, INotifyPropertyChanged
    {
        string BrakeSubSystem { get; set; }
        string FireSubSystem { get; set; }
        string InsulationSubSystem { get; set; }
        string ColumnSunSystem { get; set; }
        string RunOneSunSystem { get; set; }
        string RunTwoSunSystem { get; set; }
        string VideoSunSystem { get; set; }
        string SleepSunSystem { get; set; }
    }
}