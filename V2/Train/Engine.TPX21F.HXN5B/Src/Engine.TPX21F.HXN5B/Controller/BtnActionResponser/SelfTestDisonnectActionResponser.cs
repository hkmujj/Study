using System.ComponentModel.Composition;
using MMI.Facility.WPFInfrastructure.Event;
using MMI.Facility.WPFInfrastructure.Interactivity;

namespace Engine.TPX21F.HXN5B.Controller.BtnActionResponser
{
    [Export]
    public class SelfTestDisonnectActionResponser : BtnActionResponserBase
    {
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

        protected void SendDisconnectData(bool state)
        {
            var mm = ViewModel.Value.Domain.TestViewModel.TestSelfViewModel;
            if (mm.Model.SelectedItem != null && mm.Model.SelectedItem.ItemConfig.DiscnontectIndexKey != null)
            {
                EventAggregator.GetEvent<SendDataRequestEvent>()
                    .Publish(new SendDataRequestEvent.Args(mm.Model.SelectedItem.ItemConfig.DiscnontectIndexKey, state));
            }
        }
    }
}