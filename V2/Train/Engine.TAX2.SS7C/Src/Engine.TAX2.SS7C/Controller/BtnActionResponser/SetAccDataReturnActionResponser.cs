using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class SetAccDataReturnActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            if (!ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Model.HasAnyModified)
            {
                NavigateBack();
            }
            else
            {
                NavigateTo(StateKeys.Root_������ʾ_��������_���ۼƲ���ȷ��ȡ���޸�);
                ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Model.IsSureModify = true;
            }
        }
    }
}