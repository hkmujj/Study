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
using Motor.ATP._200H.Subsys.Constant;
using Motor.ATP._200H.Subsys.Model;

//.PopupView;

namespace Motor.ATP._200H.Subsys.ViewModel.PopupViewModels
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


        public InputDataTrainIDPopupViewModel()
        {
            m_DriverInputInterpreter = new DriverInputDataInterpreter();
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();

            PopupViewName = PopupContentViewNames.InputTrainIDView;
            TitleContent = PopupViewStringKeys.StringTrainIdModify;
            PopViewTitleContent = PopupViewStringKeys.StringTitleInputingTrainId;
        }




        public override void ResponseAction(IDriverInterface driverInterface)
        {

            TrainId = ATP.TrainInfo.Driver.TrainId;

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

                        EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainId>>()
                            .Publish(
                                new DriverInputEventArgs<DriverInputTrainId>(
                                    new DriverInputTrainId(TrainId), ATP));
                        break;
                    case DriverInputControlWord.Cancel:

                        break;
                    case DriverInputControlWord.Delete:
                        if (TrainId.Length > 1)
                        {
                            TrainId = TrainId.Substring(0, TrainId.Length - 1);
                        }
                        else
                        {
                            TrainId = string.Empty;
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
                    UpdateId(rlt.InputContent);
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    UpdateId(rlt.InputContent);
                    break;
                case DriverInputInterpreterResult.InputType.Control:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool UpdateId(string c)
        {
            lock (m_Locker)
            {
                TrainId += c;

                return true;
            }
        }
    }
}