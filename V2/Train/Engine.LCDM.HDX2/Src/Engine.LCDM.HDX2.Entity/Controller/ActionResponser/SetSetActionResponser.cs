using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SetSetActionResponser : SetContentActionResponser<SetEventArg>
    {
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_Setting_OkCancel));
        }
    }
}