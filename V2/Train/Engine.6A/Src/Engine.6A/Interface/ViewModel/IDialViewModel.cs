using System.ComponentModel;
using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel
{
    public interface IDialViewModel : IClearData, INotifyPropertyChanged
    {
        ICommand ChangedView { get; }
        double HourAngle { get; set; }
        double MiuAngle { get; set; }
        double SecAngle { get; set; }
    }
}