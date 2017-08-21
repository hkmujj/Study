using System.ComponentModel.Composition;
using CommonUtil.Model;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class MoveAccDataCaretRightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.SecondLevelViewModel.SetAccDataViewModel.Controller.MoveCaret(Direction.Right);
        }
    }
}