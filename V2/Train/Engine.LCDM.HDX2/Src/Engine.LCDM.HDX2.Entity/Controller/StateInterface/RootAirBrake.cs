using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootAirBrake: StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_AirBrake); }
        }

        public RootAirBrake()
        {
            Title = ResourceKeys.AirBrake;

            
            BtnF3 = new BtnItem(ResourceKeys.More, GetActionResponser<AirBrakeMoreActionResponser>());

            var setacction = GetActionResponser<SetAirBrakeActionResponser>();
            setacction.SendEventArg =new SetAirBrakeEventArg(SetAirBrakeType.CutInOff);
            setacction.ToStateAfterResponse = StateInterfaceKey.Parser(StateKeys.Root_AirBrake_OkCancel);
            BtnF5 = new BtnItem(ResourceKeys.CutInCutOff, setacction);

            setacction = GetActionResponser<SetAirBrakeActionResponser>();
            setacction.SendEventArg = new SetAirBrakeEventArg(SetAirBrakeType.TrainType);
            setacction.ToStateAfterResponse = StateInterfaceKey.Parser(StateKeys.Root_AirBrake_OkCancel);
            BtnF6 = new BtnItem(ResourceKeys.TrainType, setacction);

            BtnF7 = new BtnItem(ResourceKeys.Maintenance, GetActionResponser<MaintenanceActionResponser>());

            SetF8Return();
        }
    }
}