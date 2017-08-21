using System.Windows.Input;
using System.Windows.Media;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface ILoginViewModel : IViewModelBase
    {
        bool IsSetUser { get; }
        string UserName { get; }
        string Password { get; }
        UserType Type { get; set; }
        ICommand TypeSet { get; }
        ICommand ButtonDown { get; }
        ICommand ViewRender { get; }
        ICommand SetChanged { get; }
        SolidColorBrush UserBrush { get; set; }
        SolidColorBrush PasswordBrush { get; set; }
    }
}