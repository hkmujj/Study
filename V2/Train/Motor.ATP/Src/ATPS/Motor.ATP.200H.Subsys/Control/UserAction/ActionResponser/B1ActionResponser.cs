using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class B1ActionResponser : DriverActionResponserBase
    {
        public B1ActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B1)
        {
        }
    }
}