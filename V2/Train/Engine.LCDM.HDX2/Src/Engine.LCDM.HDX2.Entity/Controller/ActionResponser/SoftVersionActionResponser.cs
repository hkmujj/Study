using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class SoftVersionActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            GetEvent<RequestChangeMainRegionViewEventArg>().Publish(new RequestChangeMainRegionViewEventArg(MainRegionViewType.SoftVersion));
            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_SoftVersion));
        }
    }
}