using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP._300T.Subsys.ViewModel.PopupViewModels;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkTrainIDActionResponser : F6OkActionResponser
    {
        public F6OkTrainIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
        public override void ResponseMouseUp()
        {
            //InputDataTrainIDPopupViewModel inputDataTrain = new InputDataTrainIDPopupViewModel();
            if (!string.IsNullOrEmpty(ATP.TrainInfo.Driver.InputtingTrainId))
            {
                base.ResponseMouseUp();
            }
        }
    }
}