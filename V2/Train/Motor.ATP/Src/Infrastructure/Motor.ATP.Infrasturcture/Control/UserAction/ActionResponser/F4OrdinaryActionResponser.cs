using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F4OrdinaryActionResponser : DriverActionResponserBase
    {
        public F4OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F4)
        {
        }
    }
}