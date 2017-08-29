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

//.PopupView;

namespace Motor.ATP._300T.Subsys.ViewModel.PopupViewModels
{

    [Export]
    public class InputDataTrainIDPopupViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        private string m_TrainId;
        private DriverInputState m_InputState;

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

        public InputDataTrainIDPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleDriveData;
            m_DriverInputInterpreter = new DriverInputDataInterpreter();
            //{CharSpan = new TimeSpan(0, 0, 10)};
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
            PopupViewName = PopupContentViewNames.InputTrainIDView;
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            TrainId = driverInterface.ATP.TrainInfo.Driver.TrainId;

            driverInterface.ATP.TrainInfo.Driver.IsInputtingTrainId = true;
            ATP.TrainInfo.Driver.InputtingTrainId = TrainId;

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
                    ATP.TrainInfo.Driver.InputtingTrainId = TrainId;
                    return;
                }

                rlt = m_ControlWordInterpreter.Interpreter(args.ActionType);
                if (rlt.DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
                {
                    UpdateId(rlt);
                    ATP.TrainInfo.Driver.InputtingTrainId = TrainId;
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
                        ATP.TrainInfo.Driver.IsInputtingTrainId = false;
                            DriverInterface.ATP.SendInterface.SendTrainId(
                                new SendModel<DriverDataModel>(new DriverDataModel(null, TrainId)));                       
                        break;
                    case DriverInputControlWord.Cancel:
                        DriverInterface.ATP.SendInterface.SendTrainId(
                            new SendModel<DriverDataModel>(new DriverDataModel(null, TrainId), SendModelType.Cancel));
                        break;
                    case DriverInputControlWord.Delete:
                        if (!string.IsNullOrWhiteSpace(TrainId))
                        {
                            TrainId = TrainId.Remove(TrainId.Length - 1);
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
                    TrainId += rlt.InputContent;
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    TrainId = TrainId.Substring(0, TrainId.Length - 1) + rlt.InputContent;
                    break;
                case DriverInputInterpreterResult.InputType.Control:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}