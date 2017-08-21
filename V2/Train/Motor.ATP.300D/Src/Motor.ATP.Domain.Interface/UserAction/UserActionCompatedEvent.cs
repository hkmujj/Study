//using System.Diagnostics;


//namespace Motor.ATP.Domain.Interface.UserAction
//{
//    /// <summary>
//    /// 用户动作完成.
//    /// </summary>
//    public class UserActionComplatedEvent : CompositePresentationEvent<UserActionComplatedEventArgs>
//    {
         
//    }

//    [DebuggerDisplay("ActionComplatedTarget = {ActionComplatedTarget},ActionComplatedType = {ActionComplatedType}")]
//    public class UserActionComplatedEventArgs
//    {
//        [DebuggerStepThrough]
//        public UserActionComplatedEventArgs(UserActionComplatedTarget actionComplatedTarget, UserActionComplatedType actionComplatedType, string content = null)
//        {
//            ActionComplatedTarget = actionComplatedTarget;
//            Content = content;
//            ActionComplatedType = actionComplatedType;
//        }

//        /// <summary>
//        /// 动作目标
//        /// </summary>
//        public UserActionComplatedTarget ActionComplatedTarget { private set; get; }

//        /// <summary>
//        /// 动作类型
//        /// </summary>
//        public UserActionComplatedType ActionComplatedType { private set; get; }

//        /// <summary>
//        /// 需要输入的内容
//        /// </summary>
//        public string Content { private set; get; }
//    }

//    public enum UserActionComplatedType
//    {
//        /// <summary>
//        /// 输入一个字符
//        /// </summary>
//        Input,

//        Up,

//        Down,

//        Left,

//        Right,

//        Ok,

//        Cancel,

//        Delete,
//    }

//    public enum UserActionComplatedTarget
//    {
//        Unkown,

//        DriverID,

//        TrainLine,

//        DriverIDAndTrainLine,

//        Light,

//        Voloumn,

//        LightAndVoloumn,

//        MessageList,
//    }
//}