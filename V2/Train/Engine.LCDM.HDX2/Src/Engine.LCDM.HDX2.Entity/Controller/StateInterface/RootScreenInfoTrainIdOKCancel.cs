using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootScreenInfoTrainIdOKCancel : RootScreenInfoTrainId
    {
        public override StateInterfaceKey Id
        {
            get { return StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_TrainID_OkCancel); }
        }

        public RootScreenInfoTrainIdOKCancel()
        {
            var key = StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_TrainID);
            SetF1OK(key, () => GetEvent<SetTrainIdEventArg>().Publish(new SetTrainIdEventArg(SetTrainIdType.Ok)));
            SetF2Cancel(key, () => GetEvent<SetTrainIdEventArg>().Publish(new SetTrainIdEventArg(SetTrainIdType.Cancel)));
        }
    }
}