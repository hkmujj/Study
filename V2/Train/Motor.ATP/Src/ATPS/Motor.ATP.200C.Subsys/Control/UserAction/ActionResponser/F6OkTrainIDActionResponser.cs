using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkTrainIDActionResponser : F6OkActionResponser
    {
        private string m_Inputted;

        public F6OkTrainIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainId>>()
                .Subscribe(OnInputted, ThreadOption.PublisherThread, true, args => args.ATPType == ATP.ATPType);
        }

        private void OnInputted(DriverInputEventArgs<DriverInputTrainId> obj)
        {
            m_Inputted = obj.SelectedContent.TrainId;
        }


        /// <summary>响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应</summary>
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.SendTrainId(new SendModel<DriverDataModel>(new DriverDataModel(null,m_Inputted)));

            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_数据);
        }
    }
}