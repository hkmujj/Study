using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F7DeleteActionResponser : F7OrdinaryActionResponser
    {
        public F7DeleteActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
    }
}