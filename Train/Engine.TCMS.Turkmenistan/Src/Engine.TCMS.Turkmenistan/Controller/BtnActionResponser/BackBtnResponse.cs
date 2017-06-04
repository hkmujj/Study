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
    public class BackBtnResponse : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            ServiceLocator.Current.GetInstance<DomainController>().NavigateBack();
        }
    }
    [Export]
    public class ScreenModelViewBack : IBtnActionResponser
    {
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            ServiceLocator.Current.GetInstance<DomainController>().NavigateBack();
           var navigatorToView = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>();
            if (GlobalParam.Instance.IsReconnection)
            {
                
                navigatorToView.Publish(new NavigatorToView.Args(ViewNames.ReconnectionRunparamView));
                navigatorToView.Publish(new NavigatorToView.Args(ViewNames.ReconnectionProgressBarView));
                navigatorToView.Publish(new NavigatorToView.Args(ViewNames.RecontionAxleView));
            }
            else
            {
                
                navigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentAxleView));
                navigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentProgressbarView));
                navigatorToView.Publish(new NavigatorToView.Args(ViewNames.CurrentRunparamView));
            }
        }
    }
}
