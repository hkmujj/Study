using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkDriverIDActionResponser : F6OkActionResponser
    {
        private string m_Inputted;

        public F6OkDriverIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputDriverId>>()
                           .Subscribe(OnInputted, ThreadOption.PublisherThread, false, args => args.ATPType == ATP.ATPType);
        }

        private void OnInputted(DriverInputEventArgs<DriverInputDriverId> obj)
        {
            m_Inputted = obj.SelectedContent.DriverId;
        }


        /// <summary>响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应</summary>
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.SendDriverId(new SendModel<DriverDataModel>(new DriverDataModel(m_Inputted,null)));

            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_数据);
        }
    }
}