using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F1IncreaseVolumeActionResponser : F1OrdinaryActionResponser
    {
        public F1IncreaseVolumeActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseDown()
        {
            this.ATP.Other.Volumne++;
        }
    }
}