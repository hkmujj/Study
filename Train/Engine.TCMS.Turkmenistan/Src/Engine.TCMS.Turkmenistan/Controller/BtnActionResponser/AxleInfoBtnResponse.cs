using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    [Export]
    public class AxleInfoBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            var @event = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorEvent>();
            @event.Publish(new NavigatorEvent.Args(StateKeys.Root_轴温信息));

        }
    }
}
