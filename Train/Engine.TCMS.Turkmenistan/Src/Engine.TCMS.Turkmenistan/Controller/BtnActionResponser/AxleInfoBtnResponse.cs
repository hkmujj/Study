using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model;
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
            var @event = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToState>();
            if (GlobalParam.Instance.IsReconnection)
            {
                @event.Publish(new NavigatorToState.Args(StateKeys.Root_重连信息_轴温信息));
            }
            else
            {
                @event.Publish(new NavigatorToState.Args(StateKeys.Root_本车信息_轴温信息));
            }
            

        }
    }
}
