using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class B11ActionResponser : DriverActionResponserBase
    {
        public B11ActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.B11)
        {
        }
    }
}