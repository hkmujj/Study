using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F1BrakeTestActionResponser : F1OrdinaryActionResponser
    {
        public F1BrakeTestActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
    }
}