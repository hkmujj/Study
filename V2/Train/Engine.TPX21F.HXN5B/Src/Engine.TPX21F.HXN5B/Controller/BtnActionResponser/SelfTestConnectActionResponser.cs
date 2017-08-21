using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class SelfTestConnectActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按下
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseDown(CommandParameter parameter)
        {
            SendConnectState(true);
        }

        /// <summary>
        /// 响应弹起
        /// </summary>
        /// <param name="parameter"></param>
        public override void ResponseMouseUp(CommandParameter parameter)
        {
            SendConnectState(false);
        }

        private void SendConnectState(bool state)
        {
            var mm = ViewModel.Value.Domain.TestViewModel.TestSelfViewModel;
            if (mm.Model.SelectedItem != null && mm.Model.SelectedItem.ItemConfig.CnontectIndexKey != null)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>()
                    .Publish(new SendDataRequestEvent.Args(mm.Model.SelectedItem.ItemConfig.CnontectIndexKey, state));
            }
        }
    }
}