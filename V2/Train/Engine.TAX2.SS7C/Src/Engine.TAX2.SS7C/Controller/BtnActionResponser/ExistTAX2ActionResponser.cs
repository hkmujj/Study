using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ExistTAX2ActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_��������);
            ViewModel.Value.TrainStateViewModel.TAX2ViewModel.Model.CurrentSelectedModel = null;
        }
    }
}