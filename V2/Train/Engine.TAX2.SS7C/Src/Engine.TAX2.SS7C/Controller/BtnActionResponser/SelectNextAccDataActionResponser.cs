using System.ComponentModel.Composition;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class SelectNextAccDataActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Controller.SelectNext();
        }
    }
}