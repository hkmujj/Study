using System.ComponentModel;

namespace Engine._6A.Interface.ViewModel.SystemSeting
{
    public interface IPlateformInfoViewModel : IClearData, INotifyPropertyChanged
    {
        // ReSharper disable once InconsistentNaming
        string CPUVersion { get; set; }
        string Plateform { get; set; }
        string Technology { get; set; }
        string TelNum { get; set; }
    }
}