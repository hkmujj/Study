using Engine.Angola.TCMS.Resource.Keys;
using System.ComponentModel.Composition;

namespace Engine.Angola.TCMS.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToMaintainActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_FirstLevel_Maintain);
        }
    }
}
