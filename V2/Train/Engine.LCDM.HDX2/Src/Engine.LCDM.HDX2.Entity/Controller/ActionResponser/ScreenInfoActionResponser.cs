using System.ComponentModel.Composition;
using System.Data.SqlClient;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class ScreenInfoActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_ScreenInfo));
        }
    }
}