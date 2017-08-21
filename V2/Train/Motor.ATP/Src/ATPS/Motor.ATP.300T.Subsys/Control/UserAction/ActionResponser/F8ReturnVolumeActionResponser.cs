using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnVolumeActionResponser : F8ReturnActionResponser
    {
        public F8ReturnVolumeActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
        }
    }
}
