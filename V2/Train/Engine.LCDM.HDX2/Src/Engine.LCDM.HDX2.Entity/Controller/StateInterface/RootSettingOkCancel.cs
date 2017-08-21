using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.StateInterface
{
    [StateInterfaceExport]
    public class RootSettingOkCancel : RootSetting
    {
        public override StateInterfaceKey Id { get { return StateInterfaceKey.Parser(StateKeys.Root_Setting_OkCancel); } }

        public RootSettingOkCancel()
        {
            var key = StateInterfaceKey.Parser(StateKeys.Root_Setting);
            SetF1OK(key, () => GetEvent<SetEventArg>().Publish(new SetEventArg(SetType.Ok)));
            SetF2Cancel(key, () => GetEvent<SetEventArg>().Publish(new SetEventArg(SetType.Cancel)));
        }
    }
}