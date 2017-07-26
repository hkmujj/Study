using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F1BrakeTestActionResponser : F1OrdinaryActionResponser
    {
        public F1BrakeTestActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            ATP.SendInterface.EnsurceBrakeTestSelect(new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestSelect>(BrakeTestSelect.Run)));
        }
    }
}