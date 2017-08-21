using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B11OrdinaryActionResponser : DriverActionResponserBase
    {
        public B11OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B11)
        {
        }
    }
}