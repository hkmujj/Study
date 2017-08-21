using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F8OrdinaryActionResponser : DriverActionResponserBase
    {
        public F8OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F8)
        {
        }
    }
}