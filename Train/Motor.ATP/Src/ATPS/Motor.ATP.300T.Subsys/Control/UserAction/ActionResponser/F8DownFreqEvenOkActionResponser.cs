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

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8DownFreqEvenOkActionResponser : F8OrdinaryActionResponser
    {
        public F8DownFreqEvenOkActionResponser(IDriverSelectableItem item)
            : base(item)
        {

        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GotoRoot();

            ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(TrainFreq.Down));
            
        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Unkown)));
        }
    }
}
