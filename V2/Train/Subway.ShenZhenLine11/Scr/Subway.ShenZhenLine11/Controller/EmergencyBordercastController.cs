using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.WPFInfrastructure.Interfaces;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Event;
using Subway.ShenZhenLine11.Resource;
using Subway.ShenZhenLine11.ViewModels;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace Subway.ShenZhenLine11.Controller
{
    [Export(typeof(EmergencyBordercastController))]
    public class EmergencyBordercastController : ControllerBase<EmergencyBordercastViewModel>
    {
        [ImportingConstructor]
        public EmergencyBordercastController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Next = new DelegateCommand((() => { ViewModel.Manger.NextPage(); }));
            Last = new DelegateCommand((() => { ViewModel.Manger.LastPage(); }));
            Send = new DelegateCommand(SendAction);
        }

        protected IEventAggregator EventAggregator;

        private void SendAction()
        {
            var obj = ViewModel.CurrentInfo.FirstOrDefault(f => f.IsSelcet);
            if (obj == null)
            {
                return;
            }
            EventAggregator.GetEvent<SendDataEvent<float>>().Publish(new SendDataEnvetArgs<float>()
            {
                Index = GlobalParam.Instance.IndexConfig.OutFloatDescriptionDictionary[OutFloatKeys.OutF紧急广播号],
                Value = obj.Num,
                IsClear = true
            });
        }

        public ICommand Next { get; private set; }

        public ICommand Last { get; private set; }

        public ICommand Send { get; private set; }
    }
}