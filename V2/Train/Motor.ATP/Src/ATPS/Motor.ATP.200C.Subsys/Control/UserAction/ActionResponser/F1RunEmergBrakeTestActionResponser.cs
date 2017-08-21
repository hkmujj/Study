using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F1RunEmergBrakeTestActionResponser : F1OrdinaryActionResponser
    {
        public F1RunEmergBrakeTestActionResponser(IDriverSelectableItem item) : base(item)
        {
        }


        public override void ResponseMouseUp()
        {
            ATP.SendInterface.SendBrakeTestSelect(new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestType>(BrakeTestType.EmagerceBrakeTest)));
            //ATP.SendInterface.EnsurceBrakeTestSelect(new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestType>(BrakeTestType.EmagerceBrakeTest)));
        }
    }
}