using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;
using MMI.Facility.WPFInfrastructure.Event;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class ExitSpeedTestActionResponser : ReturnActionResponser
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            base.ResponseClick();

            EventAggregator.GetEvent<SendDataRequestEvent>()
                .Publish(new SendDataRequestEvent.Args(OubKeys.机车测试_速度测试_测试中, false));
        }
    }
}