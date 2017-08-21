using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B7OrdinaryActionResponser : DriverActionResponserBase
    {
        public B7OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B7)
        {
        }
    }
}