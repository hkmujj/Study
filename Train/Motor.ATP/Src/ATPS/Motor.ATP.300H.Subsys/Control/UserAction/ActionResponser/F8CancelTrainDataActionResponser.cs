using System.Collections.ObjectModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F8CancelTrainDataActionResponser : F8ReturnActionResponser
    {
        public F8CancelTrainDataActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.SendDriverData(new SendModel<DriverDataModel>(null, SendModelType.Cancel));
        }
    }
}