using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootAirBrakeMore: StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_AirBrake_More); }
        }

        public RootAirBrakeMore()
        {
            Title = ResourceKeys.AirBrake;

            SetF2Cancel(StateInterfaceKey.Parser(StateKeys.Root_AirBrake),
                () => GetEvent<SetAirBrakeEventArg>().Publish(new SetAirBrakeEventArg(SetAirBrakeType.Cancel)));

            var seter = GetActionResponser<SetAirBrakeActionResponser>();
            seter.SendEventArg = new SetAirBrakeEventArg(SetAirBrakeType.KPa);
            seter.ToStateAfterResponse = StateInterfaceKey.Parser(StateKeys.Root_AirBrake_More_OkCancel);
            BtnF3 = new BtnItem(ResourceKeys.KP500600, seter);

            seter = GetActionResponser<SetAirBrakeActionResponser>();
            seter.SendEventArg = new SetAirBrakeEventArg(SetAirBrakeType.MakeupAir);
            seter.ToStateAfterResponse = StateInterfaceKey.Parser(StateKeys.Root_AirBrake_More_OkCancel);
            BtnF7 = new BtnItem(ResourceKeys.MakeupAndNotAir, seter);

            SetF8Return();
        }
    }
}