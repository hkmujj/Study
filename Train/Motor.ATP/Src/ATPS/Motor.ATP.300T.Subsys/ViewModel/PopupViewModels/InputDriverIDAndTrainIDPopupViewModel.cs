using System;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300T.Subsys.Constant;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class InputDriverIDAndTrainIDPopupViewModel : DriverPopupViewModelBase
    {
        private string m_DriverId;
        private string m_TrainId;

        private bool m_IsInputtingDriverId;
        private bool m_IsInputtingTrainId;

        private DriverInputState m_InputState;

        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;

        public InputDriverIDAndTrainIDPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleDriveData;
            PopupViewName = PopupContentViewNames.InputDriverIdAndTrainIdView;
            m_DriverInputInterpreter = new DriverInputDataInterpreter();
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
        }

        public bool IsInputtingDriverId
        {
            private set
            {
                if (value == m_IsInputtingDriverId)
                {
                    return;
                }

                m_IsInputtingDriverId = value;
                m_IsInputtingTrainId = !value;
                RaisePropertyChanged(() => IsInputtingDriverId);
                RaisePropertyChanged(() => IsInputtingTrainId);
            }
            get { return m_IsInputtingDriverId; }
        }


        public bool IsInputtingTrainId
        {
            get { return m_IsInputtingTrainId; }
        }

        public string DriverId
        {
            set
            {
                if (value == m_DriverId)
                {
                    return;
                }

                m_DriverId = value;
                RaisePropertyChanged(() => DriverId);
            }
            get { return m_DriverId; }
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

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            base.ResponseAction(driverInterface);

            DriverId = driverInterface.ATP.TrainInfo.Driver.DriverId;

            TrainId = driverInterface.ATP.TrainInfo.Driver.TrainId;

            driverInterface.ATP.TrainInfo.Driver.IsInputtingDriverId = true;
            driverInterface.ATP.TrainInfo.Driver.IsInputtingTrainId = true;
            ATP.TrainInfo.Driver.InputtingTrainId = TrainId;
            ATP.TrainInfo.Driver.InputtingDriverId = DriverId;

            IsInputtingDriverId = true;
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            if (args.MouseState == MouseState.MouseUp)
            {
                var rlt = m_DriverInputInterpreter.Interpreter(args.ActionType);

                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    UpdateView(rlt);
                    ATP.TrainInfo.Driver.InputtingTrainId = TrainId;
                    ATP.TrainInfo.Driver.InputtingDriverId = DriverId;
                    return;
                }

                rlt = m_ControlWordInterpreter.Interpreter(args.ActionType);
                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    UpdateId(rlt);
                    ATP.TrainInfo.Driver.InputtingTrainId = TrainId;
                    ATP.TrainInfo.Driver.InputtingDriverId = DriverId;
                    return;
                }

                switch (args.ActionType)
                {
                    case UserActionType.F1:
                        IsInputtingDriverId = true;
                        return;
                    case UserActionType.F2:
                        IsInputtingDriverId = false;
                        return;
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
                        ATP.TrainInfo.Driver.IsInputtingTrainId = false;
                        ATP.TrainInfo.Driver.IsInputtingDriverId = false;
                        DriverInterface.ATP.SendInterface.SendDriverData(
                            new SendModel<DriverDataModel>(new DriverDataModel(TrainId, DriverId)));
                        break;
                    case DriverInputControlWord.Cancel:
                        DriverInterface.ATP.SendInterface.SendDriverData(
                            new SendModel<DriverDataModel>(new DriverDataModel(TrainId, DriverId), SendModelType.Cancel));
                        break;
                    case DriverInputControlWord.Delete:
                        if (IsInputtingTrainId)
                        {
                            if (TrainId.Any())
                            {
                                TrainId = TrainId.Remove(TrainId.Length - 1);
                            }
                        }
                        else
                        {
                            if (DriverId.Any())
                            {
                                DriverId = DriverId.Remove(DriverId.Length - 1);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                m_DriverInputInterpreter.Reset();
            }
        }

        private void UpdateView(DriverInputInterpreterResult rlt)
        {
            InputState = m_DriverInputInterpreter.InputState;
            switch (rlt.DriverInputType)
            {
                case DriverInputInterpreterResult.InputType.New:
                    if (IsInputtingTrainId)
                    {
                        TrainId += rlt.InputContent;
                    }
                    else
                    {
                        DriverId += rlt.InputContent;
                    }
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    break;
                case DriverInputInterpreterResult.InputType.Control:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}