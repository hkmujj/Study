using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ChangeLightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Controller.ChangeLight();
        }
    }
}