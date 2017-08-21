using System.ComponentModel.Composition;

namespace Engine.TCMS.HXD3.Controller.BtnActionResponser
{
    [Export]
    public class ReturnActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateBack();
        }
    }
}