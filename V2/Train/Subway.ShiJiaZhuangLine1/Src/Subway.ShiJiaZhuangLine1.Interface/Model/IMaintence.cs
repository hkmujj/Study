using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Data;

namespace Subway.ShiJiaZhuangLine1.Interface.Model
{
    public interface IMaintence : IViewModel
    {
        ICommand Clear { get; }
        ICommand InputKey { get; }
        string Password { get; }
        string DispalyPassword { get; }
    }

    public class MaintenceViewModel : NotificationObject, IMaintence
    {
        private string _password;
        private string _dispalyPassword;

        public MaintenceViewModel()
        {
            Clear = new DelegateCommand(ClearMethod);
            InputKey = new DelegateCommand<string>(InputKeyMethod);
        }

        private void InputKeyMethod(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                Password = obj;
            }
            else if (Password.Length < 4)
            {
                Password += obj;
            }
        }

        private void ClearMethod()
        {
            Password = string.Empty;
        }

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }

        public ICommand Clear { get; private set; }
        public ICommand InputKey { get; private set; }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                PasswordChanged();
                RaisePropertyChanged(() => Password);
            }
        }

        private void PasswordChanged()
        {
            DispalyPassword = string.IsNullOrEmpty(Password) ? string.Empty : "*".PadLeft(Password.Length, '*');
        }
        public string DispalyPassword
        {
            get { return _dispalyPassword; }
            set
            {
                if (value == _dispalyPassword) return;
                _dispalyPassword = value;
                RaisePropertyChanged(() => DispalyPassword);
            }
        }
    }
}
