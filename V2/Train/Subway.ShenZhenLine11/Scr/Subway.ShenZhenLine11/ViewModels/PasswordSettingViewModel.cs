using System.ComponentModel.Composition;
using Subway.ShenZhenLine11.Controller;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export]
    public class PasswordSettingViewModel : SubViewModelBase
    {
        private string m_DisplayPassword;
        private string m_Password;

        [ImportingConstructor]
        public PasswordSettingViewModel(PasswordSettingController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
        }
        /// <summary>
        /// 输入的密码
        /// </summary>
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
                Controller.PasswordChanged(value);
                RaisePropertyChanged(() => Password);
            }
        }
        /// <summary>
        /// 用于显示的掩码
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
        /// 密码输入相关控制
        /// </summary>
        public PasswordSettingController Controller { get; private set; }
    }
}