using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B9ActionResponser : DriverActionResponserBase
    {
        public B9ActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B9)
        {
        }
    }
}