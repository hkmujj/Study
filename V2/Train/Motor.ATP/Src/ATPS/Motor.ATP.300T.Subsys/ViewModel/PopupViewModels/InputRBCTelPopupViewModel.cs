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
    public class InputRBCTelPopupViewModel : DriverPopupViewModelBase
    {
        private string m_Rbcid;
        private string m_TelNumber;
        private bool m_IsInputtingTelNumber;

        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        private bool m_IsInputtingRBC;

        public InputRBCTelPopupViewModel()
        {
            InputState = DriverInputState.Number;
            TitleContent = PopupViewStringKeys.StringTitleRBCData;
            PopupViewName = PopupContentViewNames.InputRBCView;
            m_DriverInputInterpreter = new DriverInputDataInterpreter(false);
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
        }

        public DriverInputState InputState { get; private set; }

        public bool IsInputtingRBC
        {
            private set
            {
                if (value == m_IsInputtingRBC)
                {
                    return;
                }

                m_IsInputtingRBC = value;
                m_IsInputtingTelNumber = !value;
                RaisePropertyChanged(() => IsInputtingRBC);
                RaisePropertyChanged(() => IsInputtingTelNumber);
            }
            get { return m_IsInputtingRBC; }
        }

        public bool IsInputtingTelNumber
        {
            get { return m_IsInputtingTelNumber; }
        }

        public string RBCID
        {
            set
            {
                if (value == m_Rbcid)
                {
                    return;
                }

                m_Rbcid = value;
                RaisePropertyChanged(() => RBCID);
            }
            get { return m_Rbcid; }
        }

        public string TelNumber
        {
            set
            {
                if (value == m_TelNumber)
                {
                    return;
                }

                m_TelNumber = value;
                RaisePropertyChanged(() => TelNumber);
            }
            get { return m_TelNumber; }
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            base.ResponseAction(driverInterface);

            RBCID = ATP.TrainInfo.ConnectState.RBCID ?? string.Empty;

            TelNumber = ATP.TrainInfo.ConnectState.TelNumber ?? string.Empty;

            IsInputtingRBC = true;
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
                    return;
                }

                switch (args.ActionType)
                {
                    case UserActionType.F1:
                        IsInputtingRBC = true;
                        return;
                    case UserActionType.F2:
                        IsInputtingRBC = false;
                        return;
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
                        ATP.SendInterface.SendRBCData(new SendModel<RBCDataModel>(new RBCDataModel(RBCID, TelNumber)));
                        break;
                    case DriverInputControlWord.Cancel:
                        ATP.SendInterface.SendRBCData(new SendModel<RBCDataModel>(new RBCDataModel(RBCID, TelNumber), SendModelType.Cancel));
                        break;
                    case DriverInputControlWord.Delete:
                        if (IsInputtingTelNumber)
                        {
                            if (TelNumber.Any())
                            {
                                TelNumber = TelNumber.Remove(Math.Max(TelNumber.Length - 1, 0));
                            }
                        }
                        else
                        {
                            if (RBCID.Any())
                            {
                                RBCID = RBCID.Remove(Math.Max(RBCID.Length - 1, 0));
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
                    if (IsInputtingTelNumber)
                    {
                        TelNumber += rlt.InputContent;
                    }
                    else
                    {
                        RBCID += rlt.InputContent;
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