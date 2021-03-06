using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkRBCIDActionResponser : F6OkActionResponser
    {
        public F6OkRBCIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应</summary>
        public override void ResponseMouseUp()
        {
            NotifyMouseEnvet(MouseState.MouseUp);
        }
    }
}