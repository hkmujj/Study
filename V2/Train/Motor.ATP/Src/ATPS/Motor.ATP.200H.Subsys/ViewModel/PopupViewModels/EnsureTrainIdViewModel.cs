using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
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
            PopViewTitleContent = PopupViewStringKeys.StringTrainId;
            TitleContent = PopupViewStringKeys.StringTitleEnsureTrainIdView;
            PopupViewName = PopupContentViewNames.EnsureTrainIdView;
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainId>>()
                      .Subscribe(OnInputted, ThreadOption.PublisherThread, true, args => args.ATPType == ATPType.ATP200H);
        }

        private void OnInputted(DriverInputEventArgs<DriverInputTrainId> obj)
        {
            TrainId = obj.SelectedContent.TrainId;
        }
    }
}