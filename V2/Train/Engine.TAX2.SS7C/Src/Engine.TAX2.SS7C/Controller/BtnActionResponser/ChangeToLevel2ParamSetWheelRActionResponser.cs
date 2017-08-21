﻿using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToLevel2ParamSetWheelRActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_二级显示_参数设置_设置轮径);

            ViewModel.Value.SecondLevelViewModel.SetWhellRViewModel.Controller.ResetSetting();
        }
    }
}