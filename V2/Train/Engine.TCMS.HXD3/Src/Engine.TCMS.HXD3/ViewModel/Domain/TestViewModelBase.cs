using System.Windows;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    /// <summary>
    /// 测试Model、Base
    /// </summary>
    public class TestViewModelBase : NotificationObject, IResetSupport
    {
        private string m_Tips;
        private Visibility m_ConfirmBtnVisibility;
        private TestState m_State;

        /// <summary>
        /// 区分5个测试
        /// </summary>
        public string StateKeyName { get; protected set; }

        /// <summary>
        /// 确认按钮是否显示
        /// </summary>
        public Visibility ConfirmBtnVisibility
        {
            get { return m_ConfirmBtnVisibility; }
            set
            {
                if (value == m_ConfirmBtnVisibility)
                {
                    return;
                }

                m_ConfirmBtnVisibility = value;
                RaisePropertyChanged(() => ConfirmBtnVisibility);
            }
        }

        public TestState State
        {
            set
            {
                if (value == m_State)
                {
                    return;
                }

                m_State = value;
                ConfirmBtnVisibility = State == TestState.Ended ? Visibility.Visible : Visibility.Hidden;
            }
            get { return m_State; }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tips
        {
            get { return m_Tips; }
            set
            {
                if (value == m_Tips)
                {
                    return;
                }

                m_Tips = value;
                RaisePropertyChanged(() => Tips);
            }
        }

        public virtual void RestartTest()
        {
            
        }

        /// <summary>
        /// 测试控制
        /// </summary>
        public TestController Controller { get; protected set; }

        public void Reset()
        {
            RestartTest();
        }
    }
}