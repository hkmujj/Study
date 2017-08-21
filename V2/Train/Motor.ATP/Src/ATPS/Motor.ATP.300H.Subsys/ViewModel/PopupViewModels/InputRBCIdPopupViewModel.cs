using System;
using System.ComponentModel.Composition;
using CommonUtil.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Control;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Service;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class InputRBCIdPopupViewModel : DriverPopupViewModelBase
    {
        private readonly IDriverInputInterpreter m_DriverInputInterpreter;
        private readonly IDriverInputInterpreter m_ControlWordInterpreter;

        private string m_RbcId;

        private DataInputtedFrom m_DataInputtedFrom;

        public InputRBCIdPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleInputingRBCNumber;
            PopupViewName = PopupContentViewNames.InputRBCIdView;

            m_DataInputtedFrom = DataInputtedFrom.MainNodeRequst;

            ServiceLocator.Current.GetInstance<IEventAggregator>()
                .GetEvent<DataInputtedFromEvent>()
                .Subscribe(OnDataInputtedFromEvent, ThreadOption.BackgroundThread, false, args => args.ATPType == ATPType.ATP300H && args.DataType == typeof(RBCDataModel));

            m_DriverInputInterpreter = new DriverInputDataInterpreter() { CharSpan = new TimeSpan(0, 0, 10) };
            m_ControlWordInterpreter = new DataInputControlWordInterpreter();
        }

        private void OnDataInputtedFromEvent(DataInputtedFromEvent.Args args)
        {
            m_DataInputtedFrom = args.InputtedFrom;
        }

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

        public string RbcId
        {
            get { return m_RbcId; }
            set
            {
                if (value == m_RbcId)
                {
                    return;
                }

                m_RbcId = value;
                RaisePropertyChanged(() => RbcId);
            }
        }

        public override void ResponseAction(IDriverInterface driverInterface)
        {
            RbcId = driverInterface.ATP.TrainInfo.ConnectState.RBCID ?? string.Empty;

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
                            new SendModel<RBCDataModel>(new RBCDataModel(RbcId, null, RBCDataType.RBCID, m_DataInputtedFrom)));
                        if (m_DataInputtedFrom == DataInputtedFrom.UserActive)
                        {
                            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_数据_RBC数据);
                        }
                        break;
                    case DriverInputControlWord.Cancel:
                        DriverInterface.ATP.SendInterface.SendRBCData(
                            new SendModel<RBCDataModel>(new RBCDataModel(RbcId, null, RBCDataType.RBCID, m_DataInputtedFrom), SendModelType.Cancel));
                        if (m_DataInputtedFrom == DataInputtedFrom.UserActive)
                        {
                            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_数据_RBC数据);
                        }
                        break;
                    case DriverInputControlWord.Delete:
                        RbcId = RbcId.Remove(RbcId.Length - 1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                m_DriverInputInterpreter.Reset();
                m_DataInputtedFrom = DataInputtedFrom.MainNodeRequst;
            }
        }

        private void UpdateView(DriverInputInterpreterResult rlt)
        {
            InputState = m_DriverInputInterpreter.InputState;
            switch (rlt.DriverInputType)
            {
                case DriverInputInterpreterResult.InputType.New:
                    RbcId += rlt.InputContent;
                    break;
                case DriverInputInterpreterResult.InputType.Replace:
                    RbcId = RbcId.Remove(RbcId.Length - 1) +
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