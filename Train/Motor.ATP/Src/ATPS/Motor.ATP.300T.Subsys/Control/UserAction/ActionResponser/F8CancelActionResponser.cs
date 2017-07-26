using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8CancelActionResponser : CancelActionResponser
    {
        public F8CancelActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
    }
}