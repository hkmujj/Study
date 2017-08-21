using System.ComponentModel.Composition;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ReturnToRootActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateToRoot();
        }
    }
}