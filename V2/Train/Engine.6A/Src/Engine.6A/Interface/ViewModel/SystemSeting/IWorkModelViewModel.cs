using System.ComponentModel;
using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel.SystemSeting
{
    public interface IWorkModelViewModel : IClearData, INotifyPropertyChanged
    {
        string WorkText { get; set; }
        ICommand WorkChanged { get; }
    }
}