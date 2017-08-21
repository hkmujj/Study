using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(ILoginViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private bool m_IsSetUser;
        private string m_Password;
        private SolidColorBrush m_PasswordBrush;
        private UserType m_Type;
        private SolidColorBrush m_UserBrush;
        private string m_UserName;

        public LoginViewModel()
        {
            TypeSet = new DelegateCommand<string>((s =>
            {
                if (string.IsNullOrEmpty(s))
                {
                    return;
                }
                Type = s.Contains("Drive") ? UserType.Driver : UserType.Maintainer;
            }));
            SetChanged = new DelegateCommand<string>((s =>
            {
                if (string.IsNullOrEmpty(s))
                {
                    return;
                }
                IsSetUser = !s.Contains("User");
            }));
            ViewRender = new DelegateCommand(() =>
            {
                IsSetUser = false;
                UserName = default(string);
                Password = default(string);
                Type = UserType.Driver;
            });
            EventAggregator.GetEvent<NavigatorEvent>()
                .Subscribe((args => { RegionManager.RequestNavigate(args.Region, args.Name); }), ThreadOption.UIThread);
            ButtonDown = new DelegateCommand<string>(ButtonDownMethod);

            UserBrush = SolidColorBrushParam.WhiteBrush;
            PasswordBrush = SolidColorBrushParam.LogicGrayBrush;
        }

        public bool IsSetUser
        {
            get { return m_IsSetUser; }
            set
            {
                if (value == m_IsSetUser)
                {
                    return;
                }
                m_IsSetUser = value;
                BackColorChanged();
                RaisePropertyChanged(() => IsSetUser);
            }
        }

        public string UserName
        {
            get { return m_UserName; }
            set
            {
                if (value == m_UserName)
                {
                    return;
                }
                m_UserName = value;
                RaisePropertyChanged(() => UserName);
            }
        }


        public string Password
        {
            get { return m_Password; }
            set
            {
                if (value == m_Password)
                {
                    return;
                }
                m_Password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public UserType Type
        {
            get { return m_Type; }
            set
            {
                if (value == m_Type)
                {
                    return;
                }
                m_Type = value;
                GlobalParam.UserType = value;
                RaisePropertyChanged(() => Type);
            }
        }

        public ICommand TypeSet { get; set; }
        public ICommand ButtonDown { get; set; }
        public ICommand ViewRender { get; set; }
        public ICommand SetChanged { get; set; }

        public SolidColorBrush UserBrush
        {
            get { return m_UserBrush; }
            set
            {
                if (Equals(value, m_UserBrush))
                {
                    return;
                }
                m_UserBrush = value;
                RaisePropertyChanged(() => UserBrush);
            }
        }

        public SolidColorBrush PasswordBrush
        {
            get { return m_PasswordBrush; }
            set
            {
                if (Equals(value, m_PasswordBrush))
                {
                    return;
                }
                m_PasswordBrush = value;
                RaisePropertyChanged(() => PasswordBrush);
            }
        }

        private void SetValue(int iPara)
        {
            var str = (IsSetUser ? (Password) : UserName) + iPara;
            if (str.Length > 8)
            {
                return;
            }
            if (IsSetUser)
            {
                Password = str;
            }
            else
            {
                UserName = str;
            }
        }

        private void Cancel()
        {
            if (IsSetUser)
            {
                var str = Password;
                if (str.Length <= 1)
                {
                    Password = "";
                    return;
                }
                Password = str.Substring(0, str.Length - 1);
            }
            else
            {
                var str = UserName;
                if (str.Length <= 1)
                {
                    UserName = "";
                    return;
                }
                UserName = str.Substring(0, str.Length - 1);
            }
        }

        private void ButtonDownMethod(string obj)
        {
            ButtonType type;
            var bl = Enum.TryParse(obj, true, out type);
            if (bl)
            {
                switch (type)
                {
                    case ButtonType.One:
                        SetValue(1);
                        break;

                    case ButtonType.Two:
                        SetValue(2);
                        break;

                    case ButtonType.Three:
                        SetValue(3);
                        break;

                    case ButtonType.Four:
                        SetValue(4);
                        break;

                    case ButtonType.Five:
                        SetValue(5);
                        break;

                    case ButtonType.Six:
                        SetValue(6);
                        break;

                    case ButtonType.Seven:
                        SetValue(7);
                        break;

                    case ButtonType.Eight:
                        SetValue(8);
                        break;

                    case ButtonType.Nine:
                        SetValue(9);
                        break;

                    case ButtonType.Zero:
                        SetValue(0);
                        break;

                    case ButtonType.Cancel:
                        Cancel();
                        break;

                    case ButtonType.Login:
                        Login();
                        break;

                    case ButtonType.Modified:
                        break;

                    case ButtonType.Quit:
                        break;

                    case ButtonType.Add:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Login()
        {
            if (
                GlobalParam.AllUserConfig.Any(
                    info =>
                        info.Value.Type == GlobalParam.UserType && info.Value.ID.Equals(UserName.ToString()) &&
                        info.Value.Password.Equals(Password.ToString())))
            {
                EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs
                {
                    Region = RegionNames.MainShellRegion,
                    Name = ControlNames.MainContentShell
                });
                GlobalParam.CurrentUserInfo =
                    GlobalParam.AllUserConfig.FirstOrDefault(
                        info => info.Value.Type == GlobalParam.UserType && info.Value.ID.Equals(UserName.ToString()) &&
                                info.Value.Password.Equals(Password.ToString())).Value;
                GlobalParam.CurrentUserName = GlobalParam.CurrentUserInfo.ID;
            }
        }

        private void BackColorChanged()
        {
            if (IsSetUser)
            {
                UserBrush = SolidColorBrushParam.LogicGrayBrush;
                PasswordBrush = SolidColorBrushParam.WhiteBrush;
            }
            else
            {
                UserBrush = SolidColorBrushParam.WhiteBrush;
                PasswordBrush = SolidColorBrushParam.LogicGrayBrush;
            }
        }
    }
}