//using System.Diagnostics;


//namespace Motor.ATP.Domain.Interface.UserAction
//{
//    public class UserActionEvent : CompositePresentationEvent<UserActionEventArgs>
//    {
//    }

//    public class UserActionEventArgs
//    {
//        [DebuggerStepThrough]
//        public UserActionEventArgs(UserActionLocation location, ScreenIdentity targetATP, UserActionType actionType)
//        {
//            Location = location;
//            TargetATP = targetATP;
//            ActionType = actionType;
//        }

//        /// <summary>
//        /// 目标
//        /// </summary>
//        public ScreenIdentity TargetATP { private set; get; }

//        /// <summary>
//        /// 位置
//        /// </summary>
//        public UserActionLocation Location { private set; get; }

//        /// <summary>
//        /// 按下 or 弹起 or other
//        /// </summary>
//        public UserActionType ActionType { private set; get; }
//    }

//    // TODO 检查是否需要.
//    public enum UserActionType
//    {
//        /// <summary>
//        /// 按下
//        /// </summary>
//        Press,
        
//        /// <summary>
//        /// 释放 
//        /// </summary>
//        Release,

//    }
//}
