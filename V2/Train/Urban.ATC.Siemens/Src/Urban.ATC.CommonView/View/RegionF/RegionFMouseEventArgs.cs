using System;
using System.Diagnostics;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.CommonView.View.RegionF
{
    public class RegionFMouseEventArgs : EventArgs
    {
        [DebuggerStepThrough]
        public RegionFMouseEventArgs(UserActionType actionType, EventArgs mouseEventArgs)
        {
            MouseEventArgs = mouseEventArgs;
            ActionType = actionType;
        }

        public UserActionType ActionType { private set; get; }

        public EventArgs MouseEventArgs { private set; get; }
    }
}