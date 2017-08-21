using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class MaintenanceActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            GetEvent<RequestChangeMainRegionViewEventArg>().Publish(new RequestChangeMainRegionViewEventArg(MainRegionViewType.Maintenance));
            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_AirBrake_Maintenance));
        }
    }
}