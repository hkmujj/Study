using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Input;
using CommonUtil.Util;
using Microsoft.Practices.Prism.Commands;
using Urban.Phillippine.View.Config;
using Urban.Phillippine.View.Constant;
using Urban.Phillippine.View.Event;
using Urban.Phillippine.View.EventArgs;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface.Enum;
using Urban.Phillippine.View.Interface.ViewModel;

namespace Urban.Phillippine.View.ViewModel
{
    [Export(typeof(IChangedPasswordViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ChangedPasswordViewModel : ViewModelBase, IChangedPasswordViewModel
    {
        private readonly bool[] m_DownList = new bool[] { false, false, false };
        private string m_UserID;
        private string m_ConfirmPassword;
        private string m_NewPassword;
        private string m_OldPassword;

        public ChangedPasswordViewModel()
        {
            ChangedSet = new DelegateCommand<string>((args) =>
             {
                 if (string.IsNullOrEmpty(args))
                 {
                     return;
                 }
                 for (var i = 0; i < m_DownList.Length; i++)
                 {
                     m_DownList[i] = false;
                 }
                 var s = args.ConvertToInt();
                 if (s < m_DownList.Length)
                 {
                     m_DownList[s] = true;
                 }
             });
            ButtonDown = new DelegateCommand<string>(ButtonDownAction);
            Visiblity = new DelegateCommand(() =>
            {
                UserID = GlobalParam.CurrentUserName;
                m_DownList[0] = true;
            });
        }

        private void SetValue(string value)
        {
            if (m_DownList[0])
            {
                SetOldPwd(value);
            }
            else if (m_DownList[1])
            {
                SetNewPwd(value);
            }
            else if (m_DownList[2])
            {
                SetConfirmPwd(value);
            }
        }

        private void SetOldPwd(string value)
        {
            if (!value.Equals(((int)ButtonType.Cancel).ToString()))
            {
                OldPassword += value;
            }
            else if (value.Equals(((int)ButtonType.Cancel).ToString()))
            {
                OldPassword = OldPassword.Length > 1 ? OldPassword.Substring(0, OldPassword.Length - 1) : string.Empty;
            }
        }

        private void SetNewPwd(string value)
        {
            if (!value.Equals(((int)ButtonType.Cancel).ToString()))
            {
                NewPassword += value;
            }
            else if (value.Equals(((int)ButtonType.Cancel).ToString()))
            {
                NewPassword = NewPassword.Length > 1 ? NewPassword.Substring(0, NewPassword.Length - 1) : string.Empty;
            }
        }

        private void SetConfirmPwd(string value)
        {
            if (!value.Equals(((int)ButtonType.Cancel).ToString()))
            {
                ConfirmPassword += value;
            }
            else if (value.Equals(((int)ButtonType.Cancel).ToString()))
            {
                ConfirmPassword = ConfirmPassword.Length > 1 ? ConfirmPassword.Substring(0, ConfirmPassword.Length - 1) : string.Empty;
            }
        }

        private void ButtonDownAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            ButtonType type = default(ButtonType);
            if (Enum.TryParse(obj, true, out type))
            {
                var tempValue = ((int)type).ToString();
                switch (type)
                {
                    case ButtonType.One:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Two:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Three:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Four:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Five:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Six:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Seven:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Eight:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Nine:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Zero:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Cancel:
                        SetValue(tempValue);
                        break;

                    case ButtonType.Login:
                        break;

                    case ButtonType.Modified:
                        ModifiedPassword();
                        break;

                    case ButtonType.Quit:
                        EventAggregator.GetEvent<NavigatorEvent>().Publish(new NavigatorEventArgs()
                        {
                            Region = RegionNames.ContentAndButtonRegion,
                            Name = ControlNames.ContentButtonShell
                        });
                        break;

                    case ButtonType.Add:
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ModifiedPassword()
        {
            if (GlobalParam.CurrentUserInfo.Password == OldPassword && NewPassword == ConfirmPassword)
            {
                var file = Path.Combine(GlobalParam.InitParam.AppConfig.AppPaths.ConfigDirectory, GlobalParam.UserFileName);
                GlobalParam.CurrentUserInfo.Password = ConfirmPassword;
                var uds = new UserConfig();
                uds.AllUser = GlobalParam.AllUserConfig.Values.ToList();
                DataSerialization.SerializeToXmlFile(uds, file);
            }
        }

        public ICommand ButtonDown { get; set; }
        public ICommand ChangedSet { get; set; }
        public ICommand Visiblity { get; set; }

        public string UserID
        {
            get { return m_UserID; }
            set
            {
                if (value == m_UserID)
                {
                    return;
                }
                m_UserID = value;
                RaisePropertyChanged(() => UserID);
            }
        }

        public string OldPassword
        {
            get { return m_OldPassword; }
            set
            {
                if (value == m_OldPassword)
                {
                    return;
                }
                m_OldPassword = value;
                RaisePropertyChanged(() => OldPassword);
            }
        }

        public string NewPassword
        {
            get { return m_NewPassword; }
            set
            {
                if (value == m_NewPassword)
                {
                    return;
                }
                m_NewPassword = value;
                RaisePropertyChanged(() => NewPassword);
            }
        }

        public string ConfirmPassword
        {
            get { return m_ConfirmPassword; }
            set
            {
                if (value == m_ConfirmPassword)
                {
                    return;
                }
                m_ConfirmPassword = value;
                RaisePropertyChanged(() => ConfirmPassword);
            }
        }
    }
}