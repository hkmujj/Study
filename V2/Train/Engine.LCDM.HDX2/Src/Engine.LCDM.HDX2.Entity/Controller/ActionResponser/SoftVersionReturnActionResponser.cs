using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class ReturnStateToRootViewToMainActionResponser : ReturnToRootActionResponser
    {
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            GetEvent<RequestChangeMainRegionViewEventArg>().Publish(new RequestChangeMainRegionViewEventArg(MainRegionViewType.MainView));
        }
    }
}