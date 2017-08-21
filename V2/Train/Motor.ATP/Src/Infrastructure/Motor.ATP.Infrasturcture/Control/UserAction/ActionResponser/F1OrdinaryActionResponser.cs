using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F1OrdinaryActionResponser : DriverActionResponserBase
    {
        public F1OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F1)
        {
        }
    }
}