using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class TrainIdActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            GetEvent<SetTrainIdEventArg>().Publish(new SetTrainIdEventArg(SetTrainIdType.Begin));
            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo_TrainID));
        }
    }
}