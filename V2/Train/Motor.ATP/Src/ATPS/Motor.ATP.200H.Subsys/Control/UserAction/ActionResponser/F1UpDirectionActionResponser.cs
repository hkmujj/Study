using System.Linq;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;
using Motor.ATP._200H.Subsys.Extension;
using Motor.ATP._200H.Subsys.Model;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F1UpDirectionActionResponser : F1OrdinaryActionResponser
    {
        public F1UpDirectionActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseDown()
        {
            GlobalParam.Instance.ButtonManager.AddPressAction(new RecoredAction(UserActionMeaning.UpFreq));
        }

        public override void ResponseMouseClick()
        {
            var last = GlobalParam.Instance.ButtonManager.LastPressActionCollection.FirstOrDefault(
              f =>
                  f.ActionMeaning == UserActionMeaning.DownFreq &&
                  f.IsInDoubleClickSpan(
                      GlobalParam.Instance.ButtonManager.LastPressActionCollection.First(
                          f2 => f2.ActionMeaning == UserActionMeaning.UpFreq)));

            if (last != null)
            {
                last =
                    GlobalParam.Instance.ButtonManager.LastRelaseActionCollection.FirstOrDefault(
                        f => f.ActionMeaning == UserActionMeaning.DownFreq);

                if (last != null)
                {
                    ATP.SendInterface.SendAssistScreenTest(
                        new SendModel<AssistScreenTestModel>(new AssistScreenTestModel()));
                }

                return;
            }

            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Up), ATP));

            InterfaceController.GotoRoot();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消载频));

        }

        public override void ResponseMouseUp()
        {
            GlobalParam.Instance.ButtonManager.AddRelaseAction(new RecoredAction(UserActionMeaning.UpFreq));
        }
    }
}