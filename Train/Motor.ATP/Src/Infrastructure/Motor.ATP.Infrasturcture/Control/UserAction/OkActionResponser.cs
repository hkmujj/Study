using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    /// <summary>
    /// 确认响应
    /// </summary>
    public class OkActionResponser : DriverActionResponserBase
    {
        public OkActionResponser(IDriverSelectableItem item, UserActionType userActionType = UserActionType.F6)
            : base(item, userActionType)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            InterfaceController.GotoRoot();
        }
    }
}