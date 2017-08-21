using System.ComponentModel.Composition;
using Subway.WuHanLine6.Controller;

namespace Subway.WuHanLine6.Models.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Export]
    public sealed class PasswordModel : ModelBase
    {
        private string m_DisplayPassword;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller">密码控制类</param>
        [ImportingConstructor]
        public PasswordModel(PasswordController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BackView { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ConfirmView { get; set; }
        /// <summary>
        /// 显示的密码
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
                if (string.IsNullOrEmpty(value))
                {
                    Controller.EmptyPassword();
                }
                RaisePropertyChanged(() => DisplayPassword);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public PasswordController Controller { get; private set; }
    }
}