﻿using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class B5ActionResponser : DriverActionResponserBase
    {
        public B5ActionResponser (IDriverSelectableItem item)
            : base(item, UserActionType.B5)
        {
        }
    }
}