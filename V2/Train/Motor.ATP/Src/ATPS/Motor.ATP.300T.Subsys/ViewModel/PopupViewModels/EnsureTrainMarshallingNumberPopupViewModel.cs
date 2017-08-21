using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureTrainMarshallingNumberPopupViewModel : EnsureEventPopupViewModelBase
    {

        private readonly List<string> m_TrainData;

        public EnsureTrainMarshallingNumberPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureTrainMarshallingNumber;
            m_TrainData = new List<string>() {"8"};
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>().Subscribe(RecvInputTrainData);
        }

        private void RecvInputTrainData(DriverInputEventArgs<DriverInputTrainData> driverInputEventArgs)
        {
            string popcontent;

            if (driverInputEventArgs.SelectedContent.InputtedTrainData.Any() && driverInputEventArgs.SelectedContent.InputtedTrainData[0] == "8辆")
            {
                popcontent = PopupViewStringKeys.StringEnsureEightTrucksContent;
                m_TrainData[0] = "8";
            }
            else
            {
                popcontent = PopupViewStringKeys.StringEnsureSixteenTrucksContent;
                m_TrainData[0] = "16";
            }

            EnsurceContent = popcontent;
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            if (args.MouseState == MouseState.MouseUp)
            {
                switch (args.ActionType)
                {
                    case UserActionType.F6:
                        ATP.SendInterface.SendTrainData(
                            new SendModel<ReadOnlyCollection<string>>(m_TrainData.AsReadOnly()));
                        break;
                    case UserActionType.F8:
                        ATP.SendInterface.SendTrainData(
                            new SendModel<ReadOnlyCollection<string>>(m_TrainData.AsReadOnly(), SendModelType.Cancel));
                        break;
                }
            }
        }
    }
}