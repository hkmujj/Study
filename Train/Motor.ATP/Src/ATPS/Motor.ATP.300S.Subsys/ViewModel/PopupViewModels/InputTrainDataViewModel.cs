using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Control.UserAction.InputDataInterpreter;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300S.Subsys.Constant;

namespace Motor.ATP._300S.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class InputTrainDataViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private string m_TrainLength;

        public string TrainLength
        {
            set
            {
                if (value == m_TrainLength)
                {
                    return;
                }

                m_TrainLength = value;
                RaisePropertyChanged(() => TrainLength);
            }
            get { return m_TrainLength; }
        }

        public InputTrainDataViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleInputingTrainData;
            m_DriverInputInterpreter = new TrainDataInputDataInterpreter()
            {
                Lengths = new[]
                {
                    ATPUIString.TrainDataLengthLong, ATPUIString.TrainDataLengthShort
                }
            };

            PopupViewName = PopupContentViewNames.InputTrainDataView;
            TrainLength = ATPUIString.TrainDataLengthLong;
        }

        protected override void UpdateState()
        {

            var length = float.IsNaN(ATP.TrainInfo.TrainLegth[0])
                ? ATPUIString.TrainDataLengthLong
                : (ATP.TrainInfo.TrainLegth[0].ToString("") == ATPUIString.TrainDataLengthShort
                    ? ATPUIString.TrainDataLengthShort
                    : ATPUIString.TrainDataLengthLong);

            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Publish(
                    new DriverInputEventArgs<DriverInputTrainData>(
                        new DriverInputTrainData(ATP.ATPType, new string[] {length})));
            UpdateView(length);
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            var content = m_DriverInputInterpreter.Interpreter(args.ActionType);
            if (content != null)
            {
                EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                    .Publish(
                        new DriverInputEventArgs<DriverInputTrainData>(new DriverInputTrainData(ATP.ATPType,
                            new string[] {content.InputContent})));
                UpdateView(content.InputContent);
            }
        }

        private void UpdateView(string content)
        {
            TrainLength = content;
        }
    }
}