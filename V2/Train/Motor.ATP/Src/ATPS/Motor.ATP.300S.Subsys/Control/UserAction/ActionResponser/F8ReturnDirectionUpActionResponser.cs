using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnDirectionUpActionResponser : F8ReturnActionResponser
    {
        public F8ReturnDirectionUpActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(TrainFreq.Up, SendModelType.Cancel));
        }
    }
}