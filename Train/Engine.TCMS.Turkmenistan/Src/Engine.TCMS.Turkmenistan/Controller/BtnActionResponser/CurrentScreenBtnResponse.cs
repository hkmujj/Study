using System.ComponentModel.Composition;
using Engine.TCMS.Turkmenistan.Constant;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.TCMS.Turkmenistan.Controller.BtnActionResponser
{
    [Export]
    public class CurrentScreenBtnResponse : IBtnActionResponser
    {
        private readonly NavigatorToView mNavigatorToView;

        public CurrentScreenBtnResponse()
        {
            mNavigatorToView = mNavigatorToView = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>();
        }
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            {
                var @event = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToState>();
                @event.Publish(new NavigatorToState.Args(StateKeys.Root_本车信息_同屏方式_本车同屏));
                mNavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentAxleView));
                mNavigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentProgressbarView));
                mNavigatorToView.Publish(new NavigatorToView.Args(ViewNames.RunParamPage1));
            }
        }
    }
}