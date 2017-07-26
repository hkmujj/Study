using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F5OrdinaryActionResponser : DriverActionResponserBase
    {
        public F5OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F5)
        {
        }
    }
}