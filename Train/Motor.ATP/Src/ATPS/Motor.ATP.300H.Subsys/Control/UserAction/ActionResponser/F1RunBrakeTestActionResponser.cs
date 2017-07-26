using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F1RunBrakeTestActionResponser : DriverActionResponserBase
    {
        public F1RunBrakeTestActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F1)
        {
        }

        public override void ResponseMouseUp()
        {
            ATP.SendInterface.SendBrakeTestSelect(new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestSelect>(BrakeTestSelect.Run)));

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_制动测试实行确认取消));
        }
    }
}