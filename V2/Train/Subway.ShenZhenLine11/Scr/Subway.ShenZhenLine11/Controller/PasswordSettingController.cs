using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.Constance;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.ViewModels;

namespace Subway.ShenZhenLine11.Controller
{
    [Export]
    public class PasswordSettingController : ControllerBase<PasswordSettingViewModel>
    {
        private Visibility m_Visibility;

        [ImportingConstructor]
        public PasswordSettingController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            InputCommand = new DelegateCommand<string>(InputCommandAction);
            ConfirmCommand = new DelegateCommand(ConfirmCommandAction);
            ConfirmBackCommand = new DelegateCommand(ConfirmBackCommandAction);
            Visibility = Visibility.Hidden;
        }

        private void ConfirmBackCommandAction()
        {
            Visibility = Visibility.Hidden;
            EventAggregator.GetEvent<DeactiveRegionEvent>().Publish(new DeactiveRegionEvent.DeActiveEventArgs()
            {
                Name = RegionNames.PasswordSetteing,
            });
        }

        private void ConfirmCommandAction()
        {
            //ToDo  维护界面输入密码 确认相关逻辑
        }

        private void InputCommandAction(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }
            if (obj.Equals("Del"))
            {
                Delete();
            }
            else
            {
                Input(obj);
            }
        }

        private void Input(string key)
        {
            //密码只能输入3位
            if (ViewModel.Password != null && ViewModel.Password.Length == 3)
            {
                return;
            }
            ViewModel.Password = ViewModel.Password + key;
        }

        private void Delete()
        {
            if (string.IsNullOrEmpty(ViewModel.Password))
            {
                return;
            }
            //删除最后一位输入的密码
            ViewModel.Password = ViewModel.Password.Length == 1 ? string.Empty : ViewModel.Password.Substring(0, ViewModel.Password.Length - 1);
        }

        public void PasswordChanged(string pws)
        {
            ViewModel.DisplayPassword = string.IsNullOrEmpty(pws) ? string.Empty : "*".PadLeft(pws.Length, '*');
        }

        public Visibility Visibility
        {
            get { return m_Visibility; }
            set
            {
                if (value == m_Visibility)
                {
                    return;
                }
                m_Visibility = value;
                RaisePropertyChanged(() => Visibility);
            }
        }

        /// <summary>
        /// 输入命令  包括数字键的输入 以及Del按键
        /// </summary>
        public ICommand InputCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }
        public ICommand ConfirmBackCommand { get; private set; }
        public IEventAggregator EventAggregator { get; private set; }
    }
}