using System.Windows;
using System.Windows.Input;

namespace Engine._6A.Interface.ViewModel
{
    public interface IStartingViewModel : IClearData
    {
        ICommand ViewDispaly { get; }
        // ReSharper disable once InconsistentNaming
        Visibility TVisibility { get; set; }
        Visibility AVisibility { get; set; }
        string LoadText { get; set; }

    }
}