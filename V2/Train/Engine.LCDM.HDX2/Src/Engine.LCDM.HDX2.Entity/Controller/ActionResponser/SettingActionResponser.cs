using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class SettingActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            GetEvent<SetEventArg>().Publish(new SetEventArg(SetType.Begin));

            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_Setting));
        }
    }
}