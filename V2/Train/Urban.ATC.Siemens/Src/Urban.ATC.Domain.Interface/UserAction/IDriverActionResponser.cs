namespace Motor.ATP.Domain.Interface.UserAction
{
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