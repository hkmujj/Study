using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300S.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureTrainDataPopupViewModel : DriverPopupViewModelBase
    {
        private string[] m_TrainId;

        public string[] TrainId
        {
            get { return m_TrainId; }
            private set
            {
                if (value == m_TrainId)
                {
                    return;
                }

                m_TrainId = value;
                RaisePropertyChanged(() => TrainId);
            }
        }

        public EnsureTrainDataPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureTrainLength;
            PopupViewName = PopupContentViewNames.InputTrainDataView;
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Subscribe(RecvInputTrainData, ThreadOption.PublisherThread, false,
                    args => args.SelectedContent.ATPType == ATPType.ATP300S);
        }

        private void RecvInputTrainData(DriverInputEventArgs<DriverInputTrainData> driverInputEventArgs)
        {
            TrainId = new string[] {driverInputEventArgs.SelectedContent.InputtedTrainData.FirstOrDefault()};
        }
    }
}