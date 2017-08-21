using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureFreqPopupViewModel : EnsureEventPopupViewModelBase
    {
        private TrainFreq m_ToSureFreq;

        public EnsureFreqPopupViewModel()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Subscribe(RecvInputFreq, ThreadOption.PublisherThread, true, args => args.ATPType == ATPType.ATP200C);
        }

        private void RecvInputFreq(DriverInputEventArgs<DriverInputFreq> driverInputEventArgs)
        {
            var popcontent = string.Empty;
            m_ToSureFreq = driverInputEventArgs.SelectedContent.InputtedTrainFreq;
            switch (driverInputEventArgs.SelectedContent.InputtedTrainFreq)
            {
                case TrainFreq.Unkown:
                    break;
                case TrainFreq.Up:
                    popcontent = PopupViewStringKeys.StringEnsureDirectionUpContent;
                    PopViewTitleContent = PopupViewStringKeys.StringUp;
                    TitleContent = PopupViewStringKeys.StringDirectionUpConfirm;
                    break;
                case TrainFreq.Down:
                    popcontent = PopupViewStringKeys.StringEnsureDirectionDownContent;
                    PopViewTitleContent = PopupViewStringKeys.StringDown;
                    TitleContent = PopupViewStringKeys.StringDirectionDownConfirm;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
                        ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(m_ToSureFreq));
                        break;
                    case UserActionType.F8:
                        ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(m_ToSureFreq, SendModelType.Cancel));
                        break;
                }
            }
        }
    }
}