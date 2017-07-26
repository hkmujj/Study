using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8UpFreqEvenOkActionResponser : F8OrdinaryActionResponser
    {
        private readonly IInfomationService m_InfomationService;
        public F8UpFreqEvenOkActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GotoRoot();


            ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(TrainFreq.Up));
            
        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Unkown)));
        }
    }
}
