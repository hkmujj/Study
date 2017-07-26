namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 司机按下的解释
    /// </summary>
    public interface IDriverInputInterpreter
    {
        /// <summary>
        /// 按下状态
        /// </summary>
        DriverInputState InputState { get; }

        /// <summary>
        /// 重置 ， 主要是按字母时。
        /// </summary>
        void Reset();

        /// <summary>
        /// 解释一个按键
        /// </summary>
        /// <param name="actionType"></param>
        /// <returns></returns>
        DriverInputInterpreterResult Interpreter(UserActionType actionType);
    }
}