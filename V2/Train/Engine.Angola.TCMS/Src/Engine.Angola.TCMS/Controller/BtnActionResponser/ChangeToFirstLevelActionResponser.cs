using Engine.Angola.TCMS.Resource.Keys;
using System.ComponentModel.Composition;

namespace Engine.Angola.TCMS.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToFirstLevelActionResponser : BtnActionResponserBase
    {
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_FirstLevel);
        }
    }
}
