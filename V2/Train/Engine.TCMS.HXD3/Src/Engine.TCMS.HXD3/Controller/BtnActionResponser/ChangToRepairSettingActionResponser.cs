using System.ComponentModel.Composition;

namespace Engine.TCMS.HXD3.Controller.BtnActionResponser
{
    [Export]
    public class ChangToRepairSettingActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            //TODO 设定界面跳转逻辑未编写，现在没有资料 暂定  点击设定按钮什么都不做
          //  NavigateTo(StateKeys.Root_检修状态_设定);
        }
    }
}