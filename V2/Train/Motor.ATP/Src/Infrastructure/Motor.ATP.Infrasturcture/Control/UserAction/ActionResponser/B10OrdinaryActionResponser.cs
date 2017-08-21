using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B10OrdinaryActionResponser : DriverActionResponserBase
    {
        public B10OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B10)
        {
        }
    }
}