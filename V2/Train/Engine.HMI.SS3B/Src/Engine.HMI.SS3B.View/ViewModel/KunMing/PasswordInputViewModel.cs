using System.Runtime.CompilerServices;
using System.Windows;

namespace Engine.HMI.SS3B.View.ViewModel.KunMing
{
    public class PasswordInputViewModel : ViewModelBase
    {
        private Visibility m_PasswordVisibility;
        private string m_DisplayPassword;

        public PasswordInputViewModel()
        {
            Reset();
        }

        public void Reset()
        {
            PasswordVisibility = Visibility.Visible;
            DisplayPassword = string.Empty;
            Password = string.Empty;
        }

        /// <summary>
        /// 显示密码
        /// </summary>
        public string DisplayPassword
        {
            get { return m_DisplayPassword; }
            set
            {
                if (value == m_DisplayPassword)
                {
                    return;
                }
                m_DisplayPassword = value;
                RaisePropertyChanged(() => DisplayPassword);
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 密码框显示
        /// </summary>
        public Visibility PasswordVisibility
        {
            get { return m_PasswordVisibility; }
            set
            {
                if (value == m_PasswordVisibility)
                {
                    return;
                }
                m_PasswordVisibility = value;
                if (value == Visibility.Visible)
                {
                    Reset();
                }
                RaisePropertyChanged(() => PasswordVisibility);
            }
        }

        /// <summary>
        /// 密码输入确认
        /// </summary>
        public void ConfirmPassword()
        {
            if (Password == "888888")
            {
                PasswordVisibility = Visibility.Hidden;
            }
        }


        public void InputChar(int num)
        {
            if (PasswordVisibility==Visibility.Visible)
            {
                Password += num;
                DisplayPassword = "?".PadLeft(Password.Length, '?');
                ConfirmPassword();
            }
        }

    }
}