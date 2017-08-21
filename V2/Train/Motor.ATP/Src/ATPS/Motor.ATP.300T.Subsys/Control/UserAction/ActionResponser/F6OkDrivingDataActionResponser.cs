using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkDrivingDataActionResponser : F6OkActionResponser
    {
        public F6OkDrivingDataActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            if (!string.IsNullOrEmpty(ATP.TrainInfo.Driver.InputtingDriverId) && !string.IsNullOrEmpty(ATP.TrainInfo.Driver.InputtingTrainId))
            {
                base.ResponseMouseUp();
            }
        }
    }
}
