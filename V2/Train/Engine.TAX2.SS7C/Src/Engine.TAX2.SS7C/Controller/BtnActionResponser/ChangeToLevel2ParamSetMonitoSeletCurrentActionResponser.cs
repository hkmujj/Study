using System.ComponentModel.Composition;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToLevel2ParamSetMonitoSeletCurrentActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.SecondLevelViewModel.SetMonitorViewModel.Controller.SureCurrent();
        }
    }
}