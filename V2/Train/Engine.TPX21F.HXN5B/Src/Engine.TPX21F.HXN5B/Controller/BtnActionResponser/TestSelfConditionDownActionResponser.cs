using System.ComponentModel.Composition;
using CommonUtil.Model;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class TestSelfConditionDownActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.Domain.TestViewModel.TestSelfViewModel.Controller.Select(Direction.Down);
        }
    }
}