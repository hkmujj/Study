using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B6OrdinaryActionResponser : DriverActionResponserBase
    {
        public B6OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B6)
        {
        }
    }
}