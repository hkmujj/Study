using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300T.Subsys.Constant;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureTrainIdViewModel : DriverPopupViewModelBase
    {
        private string m_TrainId;

        public string TrainId
        {
            private set
            {
                if (value == m_TrainId)
                {
                    return;
                }

                m_TrainId = value;
                RaisePropertyChanged(() => TrainId);
            }
            get { return m_TrainId; }
        }

        public EnsureTrainIdViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureTrainIdView;
            PopupViewName = PopupContentViewNames.EnsureTrainIdView;
        }


        public override void ResponseAction(IDriverInterface driverInterface)
        {
            TrainId = driverInterface.ATP.TrainInfo.Driver.TrainId;

            base.ResponseAction(driverInterface);
        }
    }
}