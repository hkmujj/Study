using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Engine.LCDM.HDX2.Resource;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChangeLangActionResponser : BtnActionResponserBase
    {
        public ResourceType TargetResourceType { set; get; }

        public override void ResponseMouseUp()
        {
            GetEvent<ChangeResourceEventArgs>().Publish(new ChangeResourceEventArgs(TargetResourceType));
        }
    }
}