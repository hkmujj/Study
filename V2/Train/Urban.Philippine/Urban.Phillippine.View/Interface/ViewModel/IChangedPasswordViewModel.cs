using System.Windows.Input;

namespace Urban.Phillippine.View.Interface.ViewModel
{
    public interface IChangedPasswordViewModel
    {
        ICommand ButtonDown { get; set; }
        ICommand ChangedSet { get; }
        ICommand Visiblity { get; }
        string UserID { get; set; }
        string OldPassword { get; set; }
        string NewPassword { get; set; }
        string ConfirmPassword { get; set; }
    }
}