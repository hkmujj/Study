﻿using System.ComponentModel.Composition;
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
    public class TwoScreenBtnReponse : IBtnActionResponser
    {
        private readonly NavigatorToView mNavigatorToView;

        public TwoScreenBtnReponse()
        {
            mNavigatorToView = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToView>();
        }
        public BtnItem Parent { get; set; }
        public void UpdateState()
        {

        }

        public void ResponseClick()
        {
            {
                var @event = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<NavigatorToState>();
                if (GlobalParam.Instance.IsReconnection)
                {
                    @event.Publish(new NavigatorToState.Args(StateKeys.Root_重连信息_同屏方式_双车同屏));
                }
                else
                {
                    @event.Publish(new NavigatorToState.Args(StateKeys.Root_本车信息_同屏方式_双车同屏));
                }
            
                mNavigatorToView.Publish(new NavigatorToView.Args(ViewNames.RecontionAxleView));
                mNavigatorToView.Publish(new NavigatorToView.Args(ViewNames.ReconnectionProgressBarView));
                mNavigatorToView.Publish(new NavigatorToView.Args(ViewNames.RunParamPage4));
            }
        }
    }
}