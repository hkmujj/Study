using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Control.UserAction.InputDataInterpreter;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class InputTrainDataViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private bool m_IsSigleTrain;
        private ObservableCollection<string> m_TrainLengths;

        public bool IsSigleTrain
        {
            get { return m_IsSigleTrain; }
            private set
            {
                if (value == m_IsSigleTrain)
                {
                    return;
                }
                m_IsSigleTrain = value;
                RaisePropertyChanged(() => IsSigleTrain);
            }
        }

        public ObservableCollection<string> TrainLengths
        {
            get { return m_TrainLengths; }
            private set
            {
                if (Equals(value, m_TrainLengths))
                {
                    return;
                }
                m_TrainLengths = value;
                RaisePropertyChanged(() => TrainLengths);
            }
        }

        public InputTrainDataViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleInputingTrainData;
            m_DriverInputInterpreter = new TrainDataInputDataInterpreter()
            {
                Lengths = new[]
                {
                    ATPUIString.TrainDataLengthLong,
                    ATPUIString.TrainDataLengthShort
                }
            };

            //PopupControl = new InputTrainDataControl();
            PopupViewName = PopupContentViewNames.InputTrainDataView;
            //TrainLength = ATPUIString.TrainDataLengthLong;
            TrainLengths =
                new ObservableCollection<string>(Enumerable.Repeat(ATPUIString.TrainDataLengthLong,
                    TrainInfo.MaxTrainLenghtCount));
        }

        protected override void UpdateState()
        {
            for (int i = 0; i < Math.Min(ATP.TrainInfo.TrainLegth.Count, TrainInfo.MaxTrainLenghtCount); ++i)
            {
                var ti = ATP.TrainInfo.TrainLegth[i];

                var length = float.IsNaN(ti)
                    ? ATPUIString.TrainDataLengthLong
                    : (ti.ToString("") == ATPUIString.TrainDataLengthShort
                        ? ATPUIString.TrainDataLengthShort
                        : ATPUIString.TrainDataLengthLong);

                TrainLengths[i] = length;
            }

            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Publish(
                    new DriverInputEventArgs<DriverInputTrainData>(
                        new DriverInputTrainData(ATP.ATPType,
                            IsSigleTrain ? new[] {TrainLengths[0]} : TrainLengths.ToArray())));

            UpdateView(TrainLengths);
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            var content = m_DriverInputInterpreter.Interpreter(args.ActionType);
            if (content != null)
            {
                var inputted = Enumerable.Repeat(content.InputContent, TrainLengths.Count).ToArray();

                EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                    .Publish(
                        new DriverInputEventArgs<DriverInputTrainData>(
                            new DriverInputTrainData(ATP.ATPType,
                                inputted)));
                UpdateView(inputted);
            }
        }

        private void UpdateView(IList<string> content)
        {
            if (TrainLengths == content)
            {
                return;
            }

            IsSigleTrain = ATP.TrainInfo.CurrentTrainGroupCount <= 1;

            for (int i = 0; i < Math.Min(content.Count, TrainLengths.Count); ++i)
            {
                TrainLengths[i] = content[i];
            }
        }
    }
}