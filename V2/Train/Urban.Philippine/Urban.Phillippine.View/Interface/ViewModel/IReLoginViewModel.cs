using System.Windows;
using System.Windows.Input;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IReLoginViewModel
    {
        ICommand Confirm { get; }
        ICommand Cancel { get; }
        Visibility Visibility { get; set; }
        ICommand ReLogin { get; }
    }
}