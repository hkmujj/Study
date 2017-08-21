using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Control.UserAction.InputDataInterpreter;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Resources;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class InputTrainDataViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;

        public bool IsSigleTrain { get; private set; }

        public List<string> TrainLengths { get; private set; }

        public InputTrainDataViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleSelectedTrainData;
            PopViewTitleContent = PopupViewStringKeys.StringTitleInputingTrainData;
            m_DriverInputInterpreter = new TrainDataInputDataInterpreter()
            {
                Lengths = new[]
                {
                    ATPUIString.TrainDataLength8,
                    ATPUIString.TrainDataLength16
                }
            };

            PopupViewName = PopupContentViewNames.InputTrainDataView;
            TrainLengths =
                new List<string>(Enumerable.Repeat(ATPUIString.TrainDataLength8,
                    TrainInfo.MaxTrainLenghtCount));
        }

        protected override void UpdateState()
        {
            for (int i = 0; i < Math.Min(ATP.TrainInfo.TrainLegth.Count, TrainInfo.MaxTrainLenghtCount); ++i)
            {
                var ti = ATP.TrainInfo.TrainLegth[i];

                var length = float.IsNaN(ti)
                    ? ATPUIString.TrainDataLength8
                    : (ti.ToString("") == ATPUIString.TrainDataLength8
                        ? ATPUIString.TrainDataLength8
                        : ATPUIString.TrainDataLength16);

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
            if (args.MouseState == MouseState.MouseDown)
            {
                return;
            }

            var content = m_DriverInputInterpreter.Interpreter(args.ActionType);
            if (content != null)
            {
                var inputted = Enumerable.Repeat(content.InputContent, TrainLengths.Count).ToArray();

                UpdateView(inputted);

                var legs = new List<string>() {TrainLengths[0] == ATPUIString.TrainDataLength8 ? "8" : "16"};
                ATP.SendInterface.SendTrainData(new SendModel<ReadOnlyCollection<string>>(legs.AsReadOnly()));

                ATP.GetInterfaceController()
                    .UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root));
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