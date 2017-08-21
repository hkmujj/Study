using System.ComponentModel.Composition;
using CommonUtil.Model;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class MoveAccDataCaretLeftActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ��Ӧ����
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Controller.MoveCaret(Direction.Left);
        }
    }
}