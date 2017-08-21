using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F1IncreaseLightActionResponser : F1OrdinaryActionResponser
    {
        public F1IncreaseLightActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
        public override void ResponseMouseDown()
        {
            this.ATP.Other.Light += 5;
        }
    }
}