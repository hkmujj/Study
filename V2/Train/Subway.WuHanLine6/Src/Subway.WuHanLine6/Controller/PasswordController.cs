using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.WuHanLine6.Models.Model;

namespace Subway.WuHanLine6.Controller
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public sealed class PasswordController : ControllerBase<PasswordModel>
    {
        private string m_Password;

        /// <summary>
        /// 
        /// </summary>
        public PasswordController()
        {
            SetPasswordCharCommand = new DelegateCommand<string>((SetPassword));
            ClearCommand = new DelegateCommand((Clear));
        }

        private void Clear()
        {
            if (string.IsNullOrEmpty(Password))
            {
                return;
            }
            Password = Password.Length == 1 ? string.Empty : Password.Substring(0, Password.Length - 1);
        }

        private void SetPassword(string s)
        {
            if (string.IsNullOrEmpty(s) || (!string.IsNullOrEmpty(Password) && Password.Length == 4))
            {
                return;
            }
            Password += s;

        }

        //密码
        private string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                m_Password = value;
                PasswordChanged();
            }
        }

        /// <summary>
        /// 清空密码
        /// </summary>
        public void EmptyPassword()
        {
            Password = string.Empty;
        }
        private void PasswordChanged()
        {
            ViewModel.DisplayPassword = string.IsNullOrEmpty(Password) ? string.Empty : "*".PadLeft(Password.Length, '*');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanConfirm()
        {
            return Password == "8888";
        }
        /// <summary>
        /// 
        /// </summary>
        public ICommand SetPasswordCharCommand { get; private set; }

        /// <summary>
        /// 清除
        /// </summary>
        public ICommand ClearCommand { get; private set; }

    }
}