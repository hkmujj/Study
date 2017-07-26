using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F2RunNormalBrakeTestActionResponser : F2OrdinaryActionResponser
    {
        public F2RunNormalBrakeTestActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            ATP.SendInterface.SendBrakeTestSelect(
                new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestType>(BrakeTestType.NormalBrakeTest)));
            //ATP.SendInterface.EnsurceBrakeTestSelect(
            //    new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestType>(BrakeTestType.NormalBrakeTest)));
        }
    }
}