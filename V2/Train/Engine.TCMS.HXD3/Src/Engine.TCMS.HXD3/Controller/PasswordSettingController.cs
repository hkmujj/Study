using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.HXD3.Controller
{
    [Export(typeof(PasswordSettingController))]
    public class PasswordSettingController : ControllerBase<PasswordSetteingViewModel>
    {
        [ImportingConstructor]
        public PasswordSettingController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Confirm = new DelegateCommand(ConfirmAction);
            EventAggregator.GetEvent<InputKeyEvent>().Subscribe((args) =>
            {
                if (ViewModel.Parent.Parent.Model.CurrentStateInterface.Id.ToString() == StateKeys.Root_检修状态 || ViewModel.Parent.Parent.Model.CurrentStateInterface.Id.ToString().Equals(StateKeys.Root_检修状态_密码已设定))
                {
                    switch (args.State)
                    {
                        case InputKeyBoardState.Zero:
                        case InputKeyBoardState.One:
                        case InputKeyBoardState.Two:
                        case InputKeyBoardState.Three:
                        case InputKeyBoardState.Four:
                        case InputKeyBoardState.Five:
                        case InputKeyBoardState.Six:
                        case InputKeyBoardState.Seven:
                        case InputKeyBoardState.Eight:
                        case InputKeyBoardState.Nine:
                            ViewModel.Password += (int)args.State;
                            break;
                        case InputKeyBoardState.Cancel:
                            ViewModel.Password = ViewModel.Password?.Length > 1
                                ? ViewModel.Password.Substring(0, ViewModel.Password.Length - 1)
                                : string.Empty;
                            break;
                        case InputKeyBoardState.Clear:
                            ViewModel.Password = string.Empty;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            });
            EventAggregator.GetEvent<ViewChangedEvent>().Subscribe((args) =>
            {
                if (args.Id.ToString().Equals(StateKeys.Root_检修状态))
                {
                    ViewModel.Password = string.Empty;
                }
            });

        }

        private void ConfirmAction()
        {
            if (ViewModel.Password.Equals("000"))
            {
                ViewModel.Parent.Parent.Controller.NavigateTo(StateKeys.Root_检修状态_密码已设定);
            }
        }

        protected IEventAggregator EventAggregator { get; private set; }
        public ICommand Confirm { get; private set; }
    }
}