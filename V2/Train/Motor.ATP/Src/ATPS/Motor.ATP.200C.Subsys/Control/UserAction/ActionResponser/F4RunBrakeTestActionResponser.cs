using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F4RunBrakeTestActionResponser : F4OrdinaryActionResponser
    {
        public override void ResponseMouseUp()
        {
            ATP.SendInterface.SendBrakeTestSelect(new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestSelect>(BrakeTestSelect.Run)));

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_÷∆∂Ø≤‚ ‘—°‘Ò));
        }

        public F4RunBrakeTestActionResponser(IDriverSelectableItem item) : base(item)
        {
        }
    }
}