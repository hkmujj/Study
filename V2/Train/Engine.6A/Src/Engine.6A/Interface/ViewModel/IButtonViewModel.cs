using System.ComponentModel;
using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel
{
    public interface IButtonViewModel : IClearData, INotifyPropertyChanged
    {
        ICommand Navigator { get; }
    }
}