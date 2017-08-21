namespace Motor.ATP.Infrasturcture.Interface.UserAction
{
    /// <summary>
    /// 司机动作响应
    /// </summary>
    public interface IDriverActionResponser
    {
        /// <summary>
        /// 响应按键按下
        /// </summary>
        void ResponseMouseDown();
        /// <summary>
        /// 响应按键弹起
        /// </summary>
        void ResponseMouseUp();
        /// <summary>
        /// 响应按键点击
        /// </summary>
        void ResponseMouseClick();
    }
}