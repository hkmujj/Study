using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;
using Engine.TCMS.HXD3.ViewModel.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.HXD3.Controller
{
    [Export]
    public class SignalInfoController : ControllerBase<SignalInfoViewModel>
    {
        public SignalType CurrentType { get; private set; }
        public ICommand ChangedSignalType { get; private set; }
        public ICommand NextPage { get; private set; }
        public ICommand LastPage { get; private set; }

        protected IEventAggregator EventAggregator { get; private set; }

        [ImportingConstructor]
        public SignalInfoController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            LastPage = new DelegateCommand(() =>
            {
                ViewModel.Page--;
                DisplayChanged();
            });
            NextPage = new DelegateCommand(() =>
            {
                ViewModel.Page++;
                DisplayChanged();
            });
            ChangedSignalType = new DelegateCommand<string>((type =>
            {
                SignalType typ;
                if (Enum.TryParse(type, true, out typ))
                {
                    CurrentType = typ;
                    ViewModel.Page = 1;
                    DisplayChanged();
                }

            }));
            EventAggregator.GetEvent<ViewChangedEvent>().Subscribe((info) =>
            {
                if (info.Id.ToString().Equals(StateKeys.Root_检修状态_状态_信号信息))
                {
                    ViewModel.Page = 1;
                    CurrentType = SignalType.AUX1;
                    DisplayChanged();
                }
            });
        }

        private void DisplayChanged()
        {
            ViewModel.DisPlaySignalInfo =
                ViewModel.AllSignalInfo.Where(w => w.Page == ViewModel.Page & w.SignalType == CurrentType)
                    .OrderBy(o => o.Row)
                    .ThenBy(T => T.Column);
        }
    }
}