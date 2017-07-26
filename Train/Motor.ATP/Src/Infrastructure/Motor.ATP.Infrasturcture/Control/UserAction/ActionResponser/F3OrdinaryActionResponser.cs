using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F3OrdinaryActionResponser : DriverActionResponserBase
    {
        public F3OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F3)
        {
        }
    }
}