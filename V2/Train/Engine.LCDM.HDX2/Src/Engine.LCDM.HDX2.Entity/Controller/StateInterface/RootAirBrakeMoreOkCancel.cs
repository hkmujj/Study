using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootAirBrakeMoreOkCancel : RootAirBrakeMore
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_AirBrake_More_OkCancel); }
        }

        public RootAirBrakeMoreOkCancel()
        {
            var key = StateInterfaceKey.Parser(StateKeys.Root_AirBrake_More);
            SetF1OK(key,() => GetEvent<SetAirBrakeEventArg>().Publish(new SetAirBrakeEventArg(SetAirBrakeType.Ok)));
            SetF2Cancel(key, () => GetEvent<SetAirBrakeEventArg>().Publish(new SetAirBrakeEventArg(SetAirBrakeType.Cancel)));
        }
    }
}