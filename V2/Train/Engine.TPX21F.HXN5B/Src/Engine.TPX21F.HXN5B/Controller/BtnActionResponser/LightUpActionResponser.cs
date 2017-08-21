using System;
using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class LightUpActionResponser : BtnActionResponserBase
    {
        public const float MinOpacity = 0f;

        /// <summary>
        /// ÏìÓ¦°´¼ü
        /// </summary>
        public override void ResponseClick()
        {
            ViewModel.Value.OtherViewModel.Model.OpacityPercent =
                Math.Max(ViewModel.Value.OtherViewModel.Model.OpacityPercent - 0.1f, MinOpacity);
        }
    }
}