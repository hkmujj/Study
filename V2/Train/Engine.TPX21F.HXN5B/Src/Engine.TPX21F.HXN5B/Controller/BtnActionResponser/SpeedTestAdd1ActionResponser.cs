using System.ComponentModel.Composition;
using Engine.TPX21F.HXN5B.Resources.Keys;
using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class SpeedTestAdd1ActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按下
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OubKeys.机车测试_速度测试_速度加1, true));
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OubKeys.机车测试_速度测试_速度加1, false));
        }
    }
}