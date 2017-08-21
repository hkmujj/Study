using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    public class AirBrakeMoreActionResponser : BtnActionResponserBase
    {
        public override void ResponseMouseUp()
        {
            ChangeStateTo(StateInterfaceKey.Parser(StateKeys.Root_AirBrake_More));
        }
    }
}