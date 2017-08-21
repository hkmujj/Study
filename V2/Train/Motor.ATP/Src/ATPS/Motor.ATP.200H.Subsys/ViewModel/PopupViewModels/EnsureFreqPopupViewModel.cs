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
using Motor.ATP._200H.Subsys.Resources.String;

//.PopupView;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class EnsureFreqPopupViewModel : EnsureEventPopupViewModelBase
    {
        private TrainFreq m_ToSureFreq;

        public EnsureFreqPopupViewModel()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Subscribe(RecvInputFreq, ThreadOption.PublisherThread, true, args => args.ATPType == ATPType.ATP200H);
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
                    popcontent = ATP200HStringKeys.StringDirctionUpConfirm;
                    PopViewTitleContent = PopupViewStringKeys.StringDirectionUpConfirm;
                    TitleContent = PopupViewStringKeys.StringDirectionUpConfirm;
                    break;
                case TrainFreq.Down:
                    popcontent = ATP200HStringKeys.StringDirctionDownConfirm;
                    PopViewTitleContent = PopupViewStringKeys.StringDirectionDownConfirm;
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