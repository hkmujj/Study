using System;
using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class LightDownActionResponser : BtnActionResponserBase
    {
        public const float MaxOpcity = 0.8f;

        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Model.OpacityPercent =
                Math.Min(ViewModel.Value.OtherViewModel.Model.OpacityPercent + 0.1f, MaxOpcity);
        }
    }
}