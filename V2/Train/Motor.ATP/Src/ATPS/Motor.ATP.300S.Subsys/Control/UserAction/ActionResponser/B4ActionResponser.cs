using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B4ActionResponser : DriverActionResponserBase
    {
        public B4ActionResponser (IDriverSelectableItem item)
            : base(item, UserActionType.B4)
        {
        }
    }
}