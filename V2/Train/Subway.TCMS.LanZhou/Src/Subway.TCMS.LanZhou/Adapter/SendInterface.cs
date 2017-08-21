using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Service;
using Subway.TCMS.LanZhou.Model;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter
{
    [Export(typeof(ISendInterface))]
    public class SendInterface : ISendInterface
    {
        public IEventAggregator EventAggregator { private set; get; }

        public ICommunicationDataService DataService
        {
            get { return GlobalParam.Instance.InitParam.CommunicationDataService; }
        }

        public void SendACControl()
        {
        }

        public void SendSettedTemper(float temper)
        {
            DataService.WriteService.ChangeBool(156, true);
        }
    }
}