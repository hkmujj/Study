using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class ReturnToRootActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            GetEvent<RequestChangeMainRegionViewEventArg>()
                .Publish(new RequestChangeMainRegionViewEventArg(MainRegionViewType.MainView));
            
            GetEvent<RequestChangeMainFooterRegionViewEventArge>()
                .Publish(new RequestChangeMainFooterRegionViewEventArge(MainFooterRegionViewType.DateTimeInfo));

            ChangeStateTo(StateInterfaceKey.Root);
        }
    }
}