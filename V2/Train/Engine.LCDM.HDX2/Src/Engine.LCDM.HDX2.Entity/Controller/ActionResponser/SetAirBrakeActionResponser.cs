using System.ComponentModel.Composition;
using Engine.LCDM.HDX2.Entity.Events;
using Engine.LCDM.HDX2.Entity.Model.BtnStragy;
using Engine.LCDM.HDX2.Entity.Resource;

namespace Engine.LCDM.HDX2.Entity.Controller.ActionResponser
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SetAirBrakeActionResponser : SetContentActionResponser<SetAirBrakeEventArg>
    {
        public StateInterfaceKey ToStateAfterResponse { set; get; }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ChangeStateTo(ToStateAfterResponse);
        }
    }
}