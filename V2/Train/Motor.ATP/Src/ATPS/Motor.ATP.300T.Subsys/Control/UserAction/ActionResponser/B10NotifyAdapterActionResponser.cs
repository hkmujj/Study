using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class B10NotifyAdapterActionResponser : B10ActionResponser
    {
        public B10NotifyAdapterActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        /// <summary>
        /// 响应按键按下，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public override void ResponseMouseDown()
        {
            base.ResponseMouseDown();

            ATP.SendInterface.OnUserAction(UserActionType, MouseState.MouseDown);
        }

        /// <summary>
        /// 响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.OnUserAction(UserActionType, MouseState.MouseUp);
        }
    }
}