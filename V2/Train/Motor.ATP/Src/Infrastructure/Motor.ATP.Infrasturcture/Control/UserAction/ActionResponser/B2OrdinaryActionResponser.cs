using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B2OrdinaryActionResponser : DriverActionResponserBase
    {
        public B2OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B2)
        {
        }
    }
}