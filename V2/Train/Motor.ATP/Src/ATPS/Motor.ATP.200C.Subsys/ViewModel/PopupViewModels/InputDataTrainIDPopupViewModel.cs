using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200C.Subsys.Constant;
using Motor.ATP._200C.Subsys.Model;

//.PopupView;

namespace Motor.ATP._200C.Subsys.ViewModel.PopupViewModels
{

    [Export]
    public class InputDataTrainIDPopupViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;

        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        private string m_TrainId;
        private DriverInputState m_InputState;

        private readonly object m_Locker = new object();

        public DriverInputState InputState
        {
            get { return m_InputState; }
            set
            {
                if (value == m_InputState)
                {
                    return;
                }

                m_InputState = value;
                RaisePropertyChanged(() => InputState);
            }
        }

        public string TrainId
        {
            set
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

        public int CurrentInputtingIndex { get; private set; }

        public InputDataTrainIDPopupViewModel()
        {
            m_DriverInputInterpreter = new DriverInputDataInterpreter();
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();

            PopupViewName = PopupContentViewNames.InputTrainIDView;
            TitleContent = PopupViewStringKeys.StringTrainIdModify;
            PopViewTitleContent = PopupViewStringKeys.StringTrainId;
        }

        private void InstanceOnTimer1S(object sender, EventArgs eventArgs)
        {
            UpdateId(TrainId[CurrentInputtingIndex] == '-' ? "_" : "-", CurrentInputtingIndex);
        }


        public override void ResponseAction(IDriverInterface driverInterface)
        {

            TrainId = "----------";

            CurrentInputtingIndex = 0;

            UpdateId("_", CurrentInputtingIndex);

            GlobalTimer.Instance.Timer1S -= InstanceOnTimer1S;
            GlobalTimer.Instance.Timer1S += InstanceOnTimer1S;

            base.ResponseAction(driverInterface);
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            if (args.MouseState == MouseState.MouseUp)
            {
                var rlt = m_DriverInputInterpreter.Interpreter(args.ActionType);

                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    UpdateView(rlt);
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
                var word = rlt.GetControlWord();
                switch (word)
                {
                    case DriverInputControlWord.Ok:
                        GlobalTimer.Instance.Timer1S -= InstanceOnTimer1S;
                        EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainId>>()
                            .Publish(
                                new DriverInputEventArgs<DriverInputTrainId>(
                                    new DriverInputTrainId(TrainId.Remove(CurrentInputtingIndex)), ATP));
                        break;
                    case DriverInputControlWord.Cancel:
                        GlobalTimer.Instance.Timer1S -= InstanceOnTimer1S;
                        break;
                    case DriverInputControlWord.Delete:
                        CurrentInputtingIndex = Math.Max(0, CurrentInputtingIndex - 1);
                        UpdateId("_-", CurrentInputtingIndex);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void UpdateView(DriverInputInterpreterResult rlt)
        {
            InputState = m_DriverInputInterpreter.InputState;
            switch (rlt.DriverInputType)
            {
                case DriverInputInterpreterResult.InputType.New:
                    if (UpdateId(rlt.InputContent + "_", CurrentInputtingIndex))
                    {
                        CurrentInputtingIndex += rlt.InputContent.Length;
                    }
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    UpdateId(rlt.InputContent, CurrentInputtingIndex);
                    break;
                case DriverInputInterpreterResult.InputType.Control:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool UpdateId(string c, int index)
        {
            lock (m_Locker)
            {
                if (index + c.Length <= TrainId.Length)
                {
                    var tmp = TrainId.Remove(index, c.Length);
                    tmp = tmp.Insert(index, c);

                    TrainId = tmp;

                    return true;
                }

                return false;
            }
        }
    }
}