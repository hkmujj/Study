using System;
using System.Diagnostics;
using CommonUtil.Controls;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Interface.Service
{
    public interface IDriverInputEventService : IService
    {
        event Action<DriverInputEventArgs> DriverInputed;
    }


    public class DriverInputEventArgs
    {
        [DebuggerStepThrough]
        public DriverInputEventArgs(UserActionType actionType, MouseState mouseState)
        {
            ActionType = actionType;
            MouseState = mouseState;
        }

        public UserActionType ActionType { private set; get; }

        public MouseState MouseState { private set; get; }
    }
}