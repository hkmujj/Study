using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F1DrivingDataDriverIDActionResponser : F1OrdinaryActionResponser
    {
        public F1DrivingDataDriverIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            
        }
    }
}
