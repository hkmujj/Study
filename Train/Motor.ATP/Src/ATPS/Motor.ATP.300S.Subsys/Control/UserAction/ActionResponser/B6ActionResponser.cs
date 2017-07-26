using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B6ActionResponser : DriverActionResponserBase
    {
        public B6ActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B6)
        {
        }
    }
}