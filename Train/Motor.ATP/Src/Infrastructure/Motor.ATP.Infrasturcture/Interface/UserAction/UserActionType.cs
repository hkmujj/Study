namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 用户作用类型， 指示用户点击了哪个按键
    /// </summary>
    public enum UserActionType
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// F1
        /// </summary>
        F1,

        /// <summary>
        /// F2
        /// </summary>
        F2,

        /// <summary>
        /// F3
        /// </summary>
        F3,

        /// <summary>
        /// F4
        /// </summary>
        F4,

        /// <summary>
        /// F5
        /// </summary>
        F5,

        /// <summary>
        /// F6
        /// </summary>
        F6,

        /// <summary>
        /// F7
        /// </summary>
        F7,

        /// <summary>
        /// F8
        /// </summary>
        F8,

        /// <summary>
        /// B1
        /// </summary>
        B1,

        /// <summary>
        /// B2
        /// </summary>
        B2,

        /// <summary>
        /// B3
        /// </summary>
        B3,

        /// <summary>
        /// B4
        /// </summary>
        B4,

        /// <summary>
        /// B5
        /// </summary>
        B5,

        /// <summary>
        /// B6
        /// </summary>
        B6,

        /// <summary>
        /// B7
        /// </summary>
        B7,

        /// <summary>
        /// B8
        /// </summary>
        B8,

        /// <summary>
        /// B9
        /// </summary>
        B9,

        /// <summary>
        /// B10
        /// </summary>
        B10,

        /// <summary>
        /// B11
        /// </summary>
        B11, 

        Max = B11,
    }

    /// <summary>
    /// UserActionTypeExtension
    /// </summary>
    public static class UserActionTypeExtension
    {
        /// <summary>
        /// 是否输入的是数据 
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public static bool IsInputData(this UserActionType actionType)
        {
            return actionType >= UserActionType.B1 && actionType <= UserActionType.B11;
        }

        /// <summary>
        /// 是否输入的是控制 
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public static bool IsInputControl(this UserActionType actionType)
        {
            return actionType >= UserActionType.F1 && actionType <= UserActionType.F8;
        }
    }
}