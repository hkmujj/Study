using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class F2OrdinaryActionResponser : DriverActionResponserBase
    {
        public F2OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F2)
        {
        }
    }
}