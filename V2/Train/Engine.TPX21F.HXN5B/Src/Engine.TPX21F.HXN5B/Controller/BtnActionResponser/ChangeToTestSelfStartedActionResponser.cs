using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToTestSelfStartedActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_机车设置_机车测试_自测试_测试中);

            ViewModel.Value.Model.Title =
                ViewModel.Value.Domain.TestViewModel.TestSelfViewModel.Model.SelectedItem.ItemConfig.Content;
        }
    }
}