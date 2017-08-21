using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class DataDriverIDPopupViewModel : DriverPopupViewModelBase
    {

        private readonly IDriverInputInterpreter m_DriverInputInterpreter;

        private readonly IDriverInputInterpreter m_ControlWordInterpreter;
        private string m_DriverId;
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

        public DataDriverIDPopupViewModel()
        {
            m_DriverInputInterpreter = new DriverInputDataInterpreter();
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
            //PopupControl = new InputDriverIDControl();
            PopupViewName = PopupContentViewNames.InputDriverIdView;
            TitleContent = PopupViewStringKeys.StringTitleInputingDriverId;
            DriverId = "567";
        }


        public override void ResponseAction(IDriverInterface driverInterface)
        {
            DriverId = driverInterface.ATP.TrainInfo.Driver.DriverId;

            base.ResponseAction(driverInterface);
        }

        protected override void DriverInputEventServiceOnDriverInputed(DriverInputEventArgs args)
        {
            if (args.MouseState == MouseState.MouseUp)
            {
                var rlt = m_DriverInputInterpreter.Interpreter(args.ActionType);

                if (rlt .DriverInputType != DriverInputInterpreterResult.InputType.Invalidate)
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
                        DriverInterface.ATP.SendInterface.SendDriverId(new SendModel<DriverDataModel>(new DriverDataModel(DriverId,null)));
                        break;
                    case DriverInputControlWord.Cancel:
                        DriverInterface.ATP.SendInterface.SendDriverId(new SendModel<DriverDataModel>(new DriverDataModel(DriverId, null),SendModelType.Cancel));
                        break;
                    case DriverInputControlWord.Delete:
                        if (!string.IsNullOrWhiteSpace(DriverId))
                        {
                            DriverId = DriverId.Remove(DriverId.Length - 1);
                        }
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
                    DriverId += rlt.InputContent;
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    DriverId = DriverId.Remove(DriverId.Length - 1) +
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