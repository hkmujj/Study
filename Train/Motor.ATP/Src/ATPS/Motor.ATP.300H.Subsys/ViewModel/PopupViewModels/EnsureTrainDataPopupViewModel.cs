using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using CommonUtil.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    public class TrainDataShowModel : NotificationObject
    {
        private string m_Data;

        [DebuggerStepThrough]
        public TrainDataShowModel(int index)
        {
            Index = index;
        }

        public int Index { get; private set; }

        public string Data
        {
            get { return m_Data; }
            set
            {
                if (value == m_Data)
                {
                    return;
                }
                m_Data = value;
                RaisePropertyChanged(() => Data);
            }
        }
    }

    [Export]
    public class EnsureTrainDataPopupViewModel : DriverPopupViewModelBase
    {
       

        private ObservableCollection<TrainDataShowModel> m_TrainDatas;

        public ObservableCollection<TrainDataShowModel> TrainDatas
        {
            get { return m_TrainDatas; }
            private set
            {
                if (value == m_TrainDatas)
                {
                    return;
                }

                m_TrainDatas = value;
                RaisePropertyChanged(() => TrainDatas);
            }
        }

        public bool SigleVisible
        {
            get { return m_SigleVisible; }
            private set
            {
                if (value == m_SigleVisible)
                {
                    return;
                }
                m_SigleVisible = value;
                RaisePropertyChanged(() => SigleVisible);
            }
        }

        public bool MutilVisible
        {
            get { return m_MutilVisible; }
            private set
            {
                if (value == m_MutilVisible)
                {
                    return;
                }
                m_MutilVisible = value;
                RaisePropertyChanged(() => MutilVisible);
            }
        }

        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        private bool m_SigleVisible;
        private bool m_MutilVisible;

        public EnsureTrainDataPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureTrainLength;
            PopupViewName = PopupContentViewNames.EnsureTrainDataView;
            TrainDatas =
                new ObservableCollection<TrainDataShowModel>(
                    Enumerable.Range(0, TrainInfo.MaxTrainLenghtCount).Select((s, i) => new TrainDataShowModel(i+1)).ToList());
            m_DriverInputInterpreter = new DriverInputDataInterpreter() {CharSpan = new TimeSpan(0, 0, 10)};
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Subscribe(RecvInputTrainData, ThreadOption.PublisherThread, false,
                    args => args.SelectedContent.ATPType == ATPType.ATP300H);
        }

        protected override void UpdateState()
        {
            SigleVisible = ATP.TrainInfo.CurrentTrainGroupCount <= 1;
            MutilVisible = !SigleVisible;
        }

        private void RecvInputTrainData(DriverInputEventArgs<DriverInputTrainData> driverInputEventArgs)
        {
            for (int i = 0;
                i < Math.Min(TrainDatas.Count, driverInputEventArgs.SelectedContent.InputtedTrainData.Length);
                ++i)
            {
                TrainDatas[i].Data = driverInputEventArgs.SelectedContent.InputtedTrainData[i];
            }
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            if (args.MouseState == MouseState.MouseUp)
            {
                var rlt = m_DriverInputInterpreter.Interpreter(args.ActionType);

                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    return;
                }

                rlt = m_ControlWordInterpreter.Interpreter(args.ActionType);
                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    UpdateId(rlt);
                }
            }
        }

        private void UpdateId(DriverInputInterpreterResult rlt)
        {
            if (rlt.DriverInputType == DriverInputInterpreterResult.InputType.Control)
            {
                var word = (DriverInputControlWord) rlt.Tag;
                switch (word)
                {
                    case DriverInputControlWord.Ok:
                        DriverInterface.ATP.SendInterface.SendTrainData(
                            new SendModel<ReadOnlyCollection<string>>(
                                new ReadOnlyCollection<string>(TrainDatas.Select(s => s.Data).ToArray())));
                        break;
                    case DriverInputControlWord.Cancel:
                        DriverInterface.ATP.SendInterface.SendTrainData(
                            new SendModel<ReadOnlyCollection<string>>(
                                new ReadOnlyCollection<string>(TrainDatas.Select(s => s.Data).ToArray()),
                                SendModelType.Cancel));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                m_DriverInputInterpreter.Reset();
            }
        }
    }
}