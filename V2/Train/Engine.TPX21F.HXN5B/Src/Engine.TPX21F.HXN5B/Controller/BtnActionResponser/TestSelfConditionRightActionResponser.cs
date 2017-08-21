using System.ComponentModel.Composition;
using CommonUtil.Model;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class TestSelfConditionRightActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.Domain.TestViewModel.TestSelfViewModel.Controller.Select(Direction.Right);
        }
    }
}