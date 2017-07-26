using System;
using System.Diagnostics;
using CommonUtil.Controls;
using MMI.Facility.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP.Infrasturcture.Interface.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDriverInputEventService : IService
    {
        /// <summary>
        /// 
        /// </summary>
        event Action<DriverInputEventArgs> DriverInputed;
    }


    /// <summary>
    /// 
    /// </summary>
    public class DriverInputEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="mouseState"></param>
        [DebuggerStepThrough]
        public DriverInputEventArgs(UserActionType actionType, MouseState mouseState)
        {
            ActionType = actionType;
            MouseState = mouseState;
        }

        /// <summary>
        /// 
        /// </summary>
        public UserActionType ActionType { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public MouseState MouseState { private set; get; }
    }
}