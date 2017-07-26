using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B5OrdinaryActionResponser : DriverActionResponserBase
    {
        public B5OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B5)
        {
        }
    }
}