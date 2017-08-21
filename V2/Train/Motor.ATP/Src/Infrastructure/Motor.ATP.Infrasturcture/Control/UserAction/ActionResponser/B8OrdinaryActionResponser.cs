using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class B8OrdinaryActionResponser : DriverActionResponserBase
    {
        public B8OrdinaryActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B8)
        {
        }
    }
}