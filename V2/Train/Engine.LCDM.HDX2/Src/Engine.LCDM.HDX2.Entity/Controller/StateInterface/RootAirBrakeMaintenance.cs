using Engine.LCDM.HDX2.Entity.Controller.ActionResponser;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootAirBrakeMaintenance : StateInterfaceBase
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_AirBrake_Maintenance); }
        }

        public RootAirBrakeMaintenance()
        {
            Title = "Maintenance";

            SetF1OK(StateInterfaceKey.Parser(StateKeys.Root_AirBrake_Maintenance));

            BtnF2 = new BtnItem(ResourceKeys.Password1, null);
            BtnF3 = new BtnItem(ResourceKeys.Password2, null);
            BtnF4 = new BtnItem(ResourceKeys.Password3, null);
            BtnF5 = new BtnItem(ResourceKeys.Password4, null);
            BtnF6 = new BtnItem(ResourceKeys.Password5, null);
            BtnF7 = new BtnItem(ResourceKeys.Password6, null);

            SetF8Return<ReturnStateToRootViewToMainActionResponser>();
        }
    }
}