
//.PopupView;

using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._200H.Subsys.Constant;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class InputTelNumberPopupViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        public InputTelNumberPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleInputingTelNumber;
            PopupViewName = PopupContentViewNames.InputTelNumberView;
            m_DriverInputInterpreter = new DriverInputDataInterpreter() { CharSpan = new TimeSpan(0, 0, 10) };
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
        }
        private DriverInputState m_InputState;
        private string m_TelNumber;

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

        public string TelNumber
        {
            get { return m_TelNumber; }
            set
            {
                if (value == m_TelNumber)
                {
                    return;
                }

                m_TelNumber = value;
                RaisePropertyChanged(() => TelNumber);
            }
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            TelNumber = driverInterface.ATP.TrainInfo.ConnectState.TelNumber ?? string.Empty;

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
                var word = (DriverInputControlWord)rlt.Tag;
                switch (word)
                {
                    case DriverInputControlWord.Ok:
                        DriverInterface.ATP.SendInterface.SendRBCData(
                            new SendModel<RBCDataModel>(new RBCDataModel(null, TelNumber, RBCDataType.TelNumber)));
                        break;
                    case DriverInputControlWord.Cancel:
                        DriverInterface.ATP.SendInterface.SendRBCData(
                            new SendModel<RBCDataModel>(new RBCDataModel(null, TelNumber, RBCDataType.TelNumber), SendModelType.Cancel));
                        break;
                    case DriverInputControlWord.Delete:
                        TelNumber = TelNumber.Remove(TelNumber.Length - 1);
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
                    TelNumber += rlt.InputContent;
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    TelNumber = TelNumber.Remove(TelNumber.Length - 1) +
                                           rlt.InputContent;
                    break;
                case DriverInputInterpreterResult.InputType.Control:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}