using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser
{
    public class EmptyActionResponser : DriverActionResponserBase
    {
        public EmptyActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.None)
        {
        }
    }
}