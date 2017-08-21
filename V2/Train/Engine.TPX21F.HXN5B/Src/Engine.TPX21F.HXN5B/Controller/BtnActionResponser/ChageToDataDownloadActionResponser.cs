using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    class ChageToDataDownloadActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_维护模式_主页面_数据下载);
        }
    }
}