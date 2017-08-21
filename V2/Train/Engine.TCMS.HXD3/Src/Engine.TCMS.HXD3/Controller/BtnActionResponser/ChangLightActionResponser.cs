using System;
using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Model.TCMS;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.Controller.BtnActionResponser
{
    [Export]
    class ChangLightToLowActionResponser : ChangLightActionResponser
    {
        public ChangLightToLowActionResponser()
        {
            ToLevel= LightLevel.Low;
        }
    }
    [Export]
    class ChangLightToMidActionResponser : ChangLightActionResponser
    {
        public ChangLightToMidActionResponser()
        {
            ToLevel = LightLevel.Midlle;
        }
    }
    [Export]
    class ChangLightToHighActionResponser : ChangLightActionResponser
    {
        public ChangLightToHighActionResponser()
        {
            ToLevel = LightLevel.High;
        }
    }

    public class ChangLightActionResponser : BtnActionResponserBase
    {
        public LightLevel ToLevel { protected set; get; }

        public string ToStateKey { protected set; get; }

        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Model.Other.LightLevel = ToLevel;
            NavigateTo(ToStateKey);
        }

        public override void UpdateState()
        {
            switch (ToLevel)
            {
                case LightLevel.Low:
                    ToStateKey = StateKeys.Root1;
                    ViewModel.Model.Other.LightLevel = LightLevel.High;
                    break;
                case LightLevel.Midlle:
                    ToStateKey = StateKeys.Root2;
                    ViewModel.Model.Other.LightLevel = LightLevel.Low;
                    break;
                case LightLevel.High:
                    ToStateKey = StateKeys.Root3;
                    ViewModel.Model.Other.LightLevel = LightLevel.Midlle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}