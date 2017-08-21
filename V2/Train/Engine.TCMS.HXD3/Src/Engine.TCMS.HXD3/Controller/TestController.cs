using System.ComponentModel.Composition;
using System.Windows;
using Engine.TCMS.HXD3.Event;
using Engine.TCMS.HXD3.Model.BtnStragy;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.ViewModel.Domain;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.HXD3.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TestController : ControllerBase<TestViewModelBase>
    {
        protected IEventAggregator EventAggregator { get; private set; }

        public DelegateCommand Confirm { get; private set; }

        [ImportingConstructor]
        public TestController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Confirm = new DelegateCommand(() =>
            {
                ViewModel.ConfirmBtnVisibility = Visibility.Hidden;
                ViewModel.State = TestState.Started;
                ViewModel.RestartTest();
            });
            EventAggregator.GetEvent<ViewChangedEvent>().Subscribe(ViewChangedCallBack);
        }

        private void ViewChangedCallBack(IStateInterface obj)
        {
            if (obj.Id.ToString().Equals(ViewModel.StateKeyName))
            {
                ViewModel.ConfirmBtnVisibility = Visibility.Visible;
            }
        }
    }
}