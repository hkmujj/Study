using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B4OrdinaryActionResponser : DriverActionResponserBase
    {
        public B4OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B4)
        {
        }
    }
}