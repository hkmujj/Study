using System.ComponentModel.Composition;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
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