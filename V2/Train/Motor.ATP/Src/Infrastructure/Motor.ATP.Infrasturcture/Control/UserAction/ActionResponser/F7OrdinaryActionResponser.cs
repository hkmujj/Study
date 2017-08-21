using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F7OrdinaryActionResponser : DriverActionResponserBase
    {
        public F7OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F7)
        {
        }
    }
}