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
        /// ��Ӧ����
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OubKeys.��������_�ٶȲ���_�ٶȼ�1, true));
        }

        /// <summary>
        /// ��Ӧ����
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            EventAggregator.GetEvent<SendDataRequestEvent>().Publish(new SendDataRequestEvent.Args(OubKeys.��������_�ٶȲ���_�ٶȼ�1, false));
        }
    }
}