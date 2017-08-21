using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnFreqActionResponser : F8ReturnActionResponser
    {
        public F8ReturnFreqActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
        }
    }
}
