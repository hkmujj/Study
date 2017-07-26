using System.Linq;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;
using Motor.ATP._200C.Subsys.Extension;
using Motor.ATP._200C.Subsys.Model;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F2DownDirectionActionResponser : F2OrdinaryActionResponser
    {
        public F2DownDirectionActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>
        /// 响应按键按下，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public override void ResponseMouseDown()
        {
            GlobalParam.Instance.ButtonManager.AddPressAction(new RecoredAction(UserActionMeaning.DownFreq));
        }

        public override void ResponseMouseClick()
        {
            var last = GlobalParam.Instance.ButtonManager.LastPressActionCollection.FirstOrDefault(
                f =>
                    f.ActionMeaning == UserActionMeaning.UpFreq &&
                    f.IsInDoubleClickSpan(
                        GlobalParam.Instance.ButtonManager.LastPressActionCollection.First(
                            f2 => f2.ActionMeaning == UserActionMeaning.DownFreq)));

            if (last != null)
            {
                last =
                    GlobalParam.Instance.ButtonManager.LastRelaseActionCollection.FirstOrDefault(
                        f => f.ActionMeaning == UserActionMeaning.UpFreq);

                if (last != null)
                {
                    ATP.SendInterface.SendAssistScreenTest(
                        new SendModel<AssistScreenTestModel>(new AssistScreenTestModel()));
                }

                return;
            }

            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Down), ATP));

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Root);

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消载频));
        }

        public override void ResponseMouseUp()
        {
            GlobalParam.Instance.ButtonManager.AddRelaseAction(new RecoredAction(UserActionMeaning.DownFreq));
        }
    }
}