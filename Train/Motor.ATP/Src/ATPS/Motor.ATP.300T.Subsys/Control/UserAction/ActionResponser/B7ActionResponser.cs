using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class B7ActionResponser : DriverActionResponserBase
    {
        public B7ActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B7)
        {
        }
    }
}