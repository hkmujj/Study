using System.ComponentModel.Composition;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class OkSetAccDataReturnActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Controller.ApplyModify();
            NavigateBack();
        }
    }
}