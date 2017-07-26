using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkDriverIDActionResponser : F6OkActionResponser
    {
        public F6OkDriverIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            //InputDataTrainIDPopupViewModel inputDataTrain = new InputDataTrainIDPopupViewModel();
            if (!string.IsNullOrEmpty(ATP.TrainInfo.Driver.InputtingDriverId))
            {
                base.ResponseMouseUp();
            }
        }
    }
}