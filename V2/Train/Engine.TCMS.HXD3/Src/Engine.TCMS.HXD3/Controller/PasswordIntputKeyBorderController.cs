using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.HXD3.Controller
{
    [Export(typeof(PasswordIntputKeyBorderController))]
    public class PasswordIntputKeyBorderController : ControllerBase<PasswordInputKeyBoardModel>
    {
        [ImportingConstructor]
        public PasswordIntputKeyBorderController(IEventAggregator eventAggregator)
        {
            InputKey = new DelegateCommand<string>(IntputKeyAction);
            CancelKey = new DelegateCommand(CancelKeyAction);
            ClearKeys = new DelegateCommand(ClearKeysAction);
            EventAggregator = eventAggregator;
        }

        private void ClearKeysAction()
        {
            EventAggregator.GetEvent<InputKeyEvent>().Publish(new IntputKeyEventArgs(InputKeyBoardState.Clear));
        }

        private void CancelKeyAction()
        {
            EventAggregator.GetEvent<InputKeyEvent>().Publish(new IntputKeyEventArgs(InputKeyBoardState.Cancel));
        }

        private void IntputKeyAction(string obj)
        {
            InputKeyBoardState result;
            if (Enum.TryParse(obj, true, out result))
            {
                EventAggregator.GetEvent<InputKeyEvent>().Publish(new IntputKeyEventArgs(result));
            }
        }
        protected IEventAggregator EventAggregator { get; set; }
        /// <summary>
        /// 输入字符命令
        /// </summary>
        public ICommand InputKey { get; private set; }
        /// <summary>
        /// 删除字符命令
        /// </summary>
        public ICommand CancelKey { get; private set; }
        /// <summary>
        /// 清除字符命令
        /// </summary>
        public ICommand ClearKeys { get; private set; }
    }
}