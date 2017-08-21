using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class SelfTestDisonnectReturnActionResponser : SelfTestDisonnectActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateBack();
        }

        /// <summary>
        /// 响应按下
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            SendDisconnectData(true);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            SendDisconnectData(false);
        }
    }
}