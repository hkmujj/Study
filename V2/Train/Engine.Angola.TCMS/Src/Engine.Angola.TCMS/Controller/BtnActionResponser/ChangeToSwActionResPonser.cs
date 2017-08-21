using Engine.Angola.TCMS.Resource.Keys;
using System.ComponentModel.Composition;

namespace Engine.Angola.TCMS.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToSwActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_SW);
        }
    }
}
