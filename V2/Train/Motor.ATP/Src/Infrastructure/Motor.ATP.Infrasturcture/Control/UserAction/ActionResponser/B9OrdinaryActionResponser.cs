using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B9OrdinaryActionResponser : DriverActionResponserBase
    {
        public B9OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B9)
        {
        }
    }
}