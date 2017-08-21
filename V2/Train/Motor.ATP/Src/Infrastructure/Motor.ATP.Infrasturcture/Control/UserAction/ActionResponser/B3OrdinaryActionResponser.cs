using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B3OrdinaryActionResponser : DriverActionResponserBase
    {
        public B3OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B3)
        {
        }
    }
}