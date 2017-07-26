using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class B10ActionResponser : DriverActionResponserBase
    {
        public B10ActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B10)
        {
        }
    }
}