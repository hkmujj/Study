using System;
using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using Subway.ShenZhenLine11.Config;
using Subway.ShenZhenLine11.Enum;
using Subway.ShenZhenLine11.Resource;
using System.ComponentModel.Composition;
using Subway.ShenZhenLine11.Extention;
using Subway.ShenZhenLine11.Views.CommonViews;

namespace Subway.ShenZhenLine11.ViewModels
{
    [Export(typeof(MainMasterViewModel))]
    public class MainMasterViewModel : SubViewModelBase
    {
        private double m_MasterFanVoltage;
        private double m_Speed;
        private double m_LimitSpeed;
        private string m_EndStation;
        private string m_NextStation;
        private string m_CurrentStation;
        private RunWork m_RunWork;
        private RunModel m_RunModel;
        private Orientation m_RunOrientation;
        private double m_Brake;
        private double m_Traction;
        private double m_NetVoltage;
        private BorderCastModel m_BorderCastModel;
        private string m_Percentage;
        private bool m_IsActiveCar6;
        private bool m_IsActiveCar1;
        private EscapeDoorState m_Car6EscapeDoorState;
        private EscapeDoorState m_Drive2EscapeDoorState;
        private EscapeDoorState m_Drive1EscapeDoorState;
        private DriveDoorState m_RightDrive2DoorState;
        private DriveDoorState m_LeftDrive2DoorState;
        private DriveDoorState m_RightDrive1DoorState;
        private DriveDoorState m_LeftDrive1DoorState;
        private bool m_HSCBFault;
        private bool m_AirPumpFault;
        private bool m_SmokeFault;
        private bool m_TractionFault;
        private bool m_BrakeFault;
        private bool m_EmergenctTalkFault;
        private bool m_DoorFault;
        private bool m_EmergencyBordercastFault;
        private bool m_AssistFault;
        private bool m_AirConditionFault;

        public double NetVoltage
        {
            get { return m_NetVoltage; }

            set
            {
                if (value.Equals(m_NetVoltage))
                {
                    return;
                }
                m_NetVoltage = value;
                RaisePropertyChanged(() => NetVoltage);
            }
        }

        public double Traction
        {
            get { return m_Traction; }

            set
            {
                if (value.Equals(m_Traction))
                {
                    return;
                }
                m_Traction = value;
                Changed(value);
                RaisePropertyChanged(() => Traction);
            }
        }

        private void Changed(double value)
        {
            if (value > double.Epsilon)
            {
                Percentage = (int)value + "%";
            }
        }

        public double Brake
        {
            get { return m_Brake; }

            set
            {
                if (value.Equals(m_Brake))
                {
                    return;
                }
                m_Brake = value;
                Changed(value);
                RaisePropertyChanged(() => Brake);
            }
        }

        public string Percentage
        {
            get { return m_Percentage; }

            set
            {
                if (value.Equals(m_Percentage))
                {
                    return;
                }
                m_Percentage = value;
                RaisePropertyChanged(() => Percentage);
            }
        }

        public Orientation RunOrientation
        {
            get { return m_RunOrientation; }

            set
            {
                if (value == m_RunOrientation)
                {
                    return;
                }
                m_RunOrientation = value;
                RaisePropertyChanged(() => RunOrientation);
            }
        }

        public RunModel RunModel
        {
            get { return m_RunModel; }

            set
            {
                if (value == m_RunModel)
                {
                    return;
                }
                m_RunModel = value;
                RaisePropertyChanged(() => RunModel);
            }
        }

        public RunWork RunWork
        {
            get { return m_RunWork; }

            set
            {
                if (value == m_RunWork)
                {
                    return;
                }
                m_RunWork = value;
                RaisePropertyChanged(() => RunWork);
            }
        }

        public string CurrentStation
        {
            get { return m_CurrentStation; }

            set
            {
                if (value == m_CurrentStation)
                {
                    return;
                }
                m_CurrentStation = value;
                RaisePropertyChanged(() => CurrentStation);
            }
        }

        public string NextStation
        {
            get { return m_NextStation; }

            set
            {
                if (value == m_NextStation)
                {
                    return;
                }
                m_NextStation = value;
                RaisePropertyChanged(() => NextStation);
            }
        }

        public string EndStation
        {
            get { return m_EndStation; }

            set
            {
                if (value == m_EndStation)
                {
                    return;
                }
                m_EndStation = value;
                RaisePropertyChanged(() => EndStation);
            }
        }

        public double LimitSpeed
        {
            get { return m_LimitSpeed; }

            set
            {
                if (value.Equals(m_LimitSpeed))
                {
                    return;
                }
                m_LimitSpeed = value;
                RaisePropertyChanged(() => LimitSpeed);
            }
        }

        public double Speed
        {
            get { return m_Speed; }

            set
            {
                if (value.Equals(m_Speed))
                {
                    return;
                }
                m_Speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        public double MasterFanVoltage
        {
            get { return m_MasterFanVoltage; }

            set
            {
                if (value.Equals(m_MasterFanVoltage))
                {
                    return;
                }
                m_MasterFanVoltage = value;
                RaisePropertyChanged(() => MasterFanVoltage);
            }
        }

        public BorderCastModel BorderCastModel
        {
            get { return m_BorderCastModel; }

            set
            {
                if (value == m_BorderCastModel)
                {
                    return;
                }
                m_BorderCastModel = value;
                RaisePropertyChanged(() => BorderCastModel);
            }
        }

        public bool IsActiveCar1
        {
            get { return m_IsActiveCar1; }
            set
            {
                if (value == m_IsActiveCar1)
                {
                    return;
                }
                m_IsActiveCar1 = value;
                RaisePropertyChanged(() => IsActiveCar1);
            }
        }

        public bool IsActiveCar6
        {
            get { return m_IsActiveCar6; }
            set
            {
                if (value == m_IsActiveCar6)
                {
                    return;
                }
                m_IsActiveCar6 = value;
                RaisePropertyChanged(() => IsActiveCar6);
            }
        }

        public DriveDoorState LeftDrive1DoorState
        {
            get { return m_LeftDrive1DoorState; }
            set
            {
                if (value == m_LeftDrive1DoorState)
                {
                    return;
                }
                m_LeftDrive1DoorState = value;
                RaisePropertyChanged(() => LeftDrive1DoorState);
            }
        }

        public DriveDoorState RightDrive1DoorState
        {
            get { return m_RightDrive1DoorState; }
            set
            {
                if (value == m_RightDrive1DoorState)
                {
                    return;
                }
                m_RightDrive1DoorState = value;
                RaisePropertyChanged(() => RightDrive1DoorState);
            }
        }

        public DriveDoorState LeftDrive2DoorState
        {
            get { return m_LeftDrive2DoorState; }
            set
            {
                if (value == m_LeftDrive2DoorState)
                {
                    return;
                }
                m_LeftDrive2DoorState = value;
                RaisePropertyChanged(() => LeftDrive2DoorState);
            }
        }

        public DriveDoorState RightDrive2DoorState
        {
            get { return m_RightDrive2DoorState; }
            set
            {
                if (value == m_RightDrive2DoorState)
                {
                    return;
                }
                m_RightDrive2DoorState = value;
                RaisePropertyChanged(() => RightDrive2DoorState);
            }
        }

        public EscapeDoorState Drive1EscapeDoorState
        {
            get { return m_Drive1EscapeDoorState; }
            set
            {
                if (value == m_Drive1EscapeDoorState)
                {
                    return;
                }
                m_Drive1EscapeDoorState = value;
                RaisePropertyChanged(() => Drive1EscapeDoorState);
            }
        }

        public EscapeDoorState Drive2EscapeDoorState
        {
            get { return m_Drive2EscapeDoorState; }
            set
            {
                if (value == m_Drive2EscapeDoorState)
                {
                    return;
                }
                m_Drive2EscapeDoorState = value;
                RaisePropertyChanged(() => Drive2EscapeDoorState);
            }
        }

        public EscapeDoorState Car6EscapeDoorState
        {
            get { return m_Car6EscapeDoorState; }
            set
            {
                if (value == m_Car6EscapeDoorState)
                {
                    return;
                }
                m_Car6EscapeDoorState = value;
                RaisePropertyChanged(() => Car6EscapeDoorState);
            }
        }

        public bool AirConditionFault
        {
            get { return m_AirConditionFault; }
            set
            {
                if (value == m_AirConditionFault)
                {
                    return;
                }
                m_AirConditionFault = value;
                RaisePropertyChanged(() => AirConditionFault);
            }
        }

        public bool AssistFault
        {
            get { return m_AssistFault; }
            set
            {
                if (value == m_AssistFault)
                {
                    return;
                }
                m_AssistFault = value;
                RaisePropertyChanged(() => AssistFault);
            }
        }

        public bool EmergencyBordercastFault
        {
            get { return m_EmergencyBordercastFault; }
            set
            {
                if (value == m_EmergencyBordercastFault)
                {
                    return;
                }
                m_EmergencyBordercastFault = value;
                RaisePropertyChanged(() => EmergencyBordercastFault);
            }
        }

        public bool DoorFault
        {
            get { return m_DoorFault; }
            set
            {
                if (value == m_DoorFault)
                {
                    return;
                }
                m_DoorFault = value;
                RaisePropertyChanged(() => DoorFault);
            }
        }

        public bool EmergenctTalkFault
        {
            get { return m_EmergenctTalkFault; }
            set
            {
                if (value == m_EmergenctTalkFault)
                {
                    return;
                }
                m_EmergenctTalkFault = value;
                RaisePropertyChanged(() => EmergenctTalkFault);
            }
        }

        public bool BrakeFault
        {
            get { return m_BrakeFault; }
            set
            {
                if (value == m_BrakeFault)
                {
                    return;
                }
                m_BrakeFault = value;
                RaisePropertyChanged(() => BrakeFault);
            }
        }

        public bool TractionFault
        {
            get { return m_TractionFault; }
            set
            {
                if (value == m_TractionFault)
                {
                    return;
                }
                m_TractionFault = value;
                RaisePropertyChanged(() => TractionFault);
            }
        }

        public bool SmokeFault
        {
            get { return m_SmokeFault; }
            set
            {
                if (value == m_SmokeFault)
                {
                    return;
                }
                m_SmokeFault = value;
                RaisePropertyChanged(() => SmokeFault);
            }
        }

        public bool AirPumpFault
        {
            get { return m_AirPumpFault; }
            set
            {
                if (value == m_AirPumpFault)
                {
                    return;
                }
                m_AirPumpFault = value;
                RaisePropertyChanged(() => AirPumpFault);
            }
        }

        public bool HSCBFault
        {
            get { return m_HSCBFault; }
            set
            {
                if (value == m_HSCBFault)
                {
                    return;
                }
                m_HSCBFault = value;
                RaisePropertyChanged(() => HSCBFault);
            }
        }

        public override void Changed(object sender, CommunicationDataChangedArgs<bool> args)
        {
            InBoolKeys.司机室激活1.IfContanin(b => IsActiveCar1 = b);
            InBoolKeys.司机室激活6.IfContanin(b => IsActiveCar6 = b);
            var dic = new Dictionary<string, Action>();
            dic.Add(InBoolKeys.方向向前, (() => RunOrientation = Orientation.Before));
            dic.Add(InBoolKeys.方向向后, (() => RunOrientation = Orientation.Back));
            dic.Add(InBoolKeys.方向未知, (() => RunOrientation = Orientation.UnKnow));
            dic.Add(InBoolKeys.方向零位, (() => RunOrientation = Orientation.Zero));
            dic.UpdateAllState();
            var dic1 = new Dictionary<string, Action>();
            dic1.Add(InBoolKeys.模式ATO, () => RunModel = RunModel.ATO);
            dic1.Add(InBoolKeys.模式AMS, () => RunModel = RunModel.AMC);
            dic1.Add(InBoolKeys.模式MCS, () => RunModel = RunModel.MCS);
            dic1.Add(InBoolKeys.模式RM, () => RunModel = RunModel.RM);
            dic1.Add(InBoolKeys.模式NRM, () => RunModel = RunModel.NRM);
            dic1.Add(InBoolKeys.模式ATB, () => RunModel = RunModel.ATB);
            dic1.Add(InBoolKeys.模式BM, () => RunModel = RunModel.BM);
            dic1.Add(InBoolKeys.模式ATP切除, () => RunModel = RunModel.ATPCut);
            dic1.Add(InBoolKeys.模式洗车模式, () => RunModel = RunModel.Wash);
            dic1.Add(InBoolKeys.模式预留1, () => RunModel = RunModel.Reserve1);
            dic1.Add(InBoolKeys.模式未知, () => RunModel = RunModel.UnKnow);
            dic1.Add(InBoolKeys.模式预留2, () => RunModel = RunModel.Reserve2);
            dic1.UpdateAllState();
            var dic2 = new Dictionary<string, Action>();
            dic2.Add(InBoolKeys.工况牵引, () => RunWork = RunWork.Traction);
            dic2.Add(InBoolKeys.工况制动, () => RunWork = RunWork.Brake);
            dic2.Add(InBoolKeys.工况快速制动, () => RunWork = RunWork.FastBrake);
            dic2.Add(InBoolKeys.工况紧急制动, () => RunWork = RunWork.EmergencyBrake);
            dic2.Add(InBoolKeys.工况惰行, () => RunWork = RunWork.Coasting);
            dic2.Add(InBoolKeys.工况停放制动, () => RunWork = RunWork.ParkingBrake);
            dic2.Add(InBoolKeys.工况紧急牵引, () => RunWork = RunWork.EmergencyTraction);
            dic2.Add(InBoolKeys.工况保持制动, () => RunWork = RunWork.KeepBrake);
            dic2.Add(InBoolKeys.工况预留2, () => RunWork = RunWork.Reserve2);
            dic2.Add(InBoolKeys.工况未知, () => RunWork = RunWork.UnKnow);
            dic2.UpdateAllState();

            var dic3 = new Dictionary<string, Action>();
            dic3.Add(InBoolKeys.司机室1右门打开, () => RightDrive1DoorState = DriveDoorState.Open);
            dic3.Add(InBoolKeys.司机室1右门关闭, () => RightDrive1DoorState = DriveDoorState.Close);
            dic3.Add(InBoolKeys.司机室1右门故障, () => RightDrive1DoorState = DriveDoorState.Falut);
            dic3.UpdateAllState(() => RightDrive1DoorState = DriveDoorState.Normal);

            var dic4 = new Dictionary<string, Action>();
            dic4.Add(InBoolKeys.司机室1左门打开, () => LeftDrive1DoorState = DriveDoorState.Open);
            dic4.Add(InBoolKeys.司机室1左门关闭, () => LeftDrive1DoorState = DriveDoorState.Close);
            dic4.Add(InBoolKeys.司机室1左门故障, () => LeftDrive1DoorState = DriveDoorState.Falut);
            dic4.UpdateAllState(() => LeftDrive1DoorState = DriveDoorState.Normal);

            var dic5 = new Dictionary<string, Action>();
            dic5.Add(InBoolKeys.司机室2右门打开, () => RightDrive2DoorState = DriveDoorState.Open);
            dic5.Add(InBoolKeys.司机室2右门关闭, () => RightDrive2DoorState = DriveDoorState.Close);
            dic5.Add(InBoolKeys.司机室2右门故障, () => RightDrive2DoorState = DriveDoorState.Falut);
            dic5.UpdateAllState(() => RightDrive2DoorState = DriveDoorState.Normal);

            var dic6 = new Dictionary<string, Action>();
            dic6.Add(InBoolKeys.司机室2左门打开, () => LeftDrive2DoorState = DriveDoorState.Open);
            dic6.Add(InBoolKeys.司机室2左门关闭, () => LeftDrive2DoorState = DriveDoorState.Close);
            dic6.Add(InBoolKeys.司机室2左门故障, () => LeftDrive2DoorState = DriveDoorState.Falut);
            dic6.UpdateAllState(() => LeftDrive2DoorState = DriveDoorState.Normal);

            var dic7 = new Dictionary<string, Action>();
            dic7.Add(InBoolKeys.司机室1通道门未锁, (() => Drive1EscapeDoorState = EscapeDoorState.UnLock));
            dic7.Add(InBoolKeys.司机室1通道门锁上, (() => Drive1EscapeDoorState = EscapeDoorState.Lock));
            dic7.UpdateAllState((() => Drive1EscapeDoorState = EscapeDoorState.Normal));

            var dic8 = new Dictionary<string, Action>();
            dic8.Add(InBoolKeys.司机室2通道门未锁, (() => Drive2EscapeDoorState = EscapeDoorState.UnLock));
            dic8.Add(InBoolKeys.司机室2通道门锁上, (() => Drive2EscapeDoorState = EscapeDoorState.Lock));
            dic8.UpdateAllState((() => Drive2EscapeDoorState = EscapeDoorState.Normal));
            var dic9 = new Dictionary<string, Action>();
            dic9.Add(InBoolKeys.客室6通道门未锁, (() => Car6EscapeDoorState = EscapeDoorState.UnLock));
            dic9.Add(InBoolKeys.客室6通道门锁上, (() => Car6EscapeDoorState = EscapeDoorState.Lock));
            dic9.UpdateAllState((() => Car6EscapeDoorState = EscapeDoorState.Normal));

            InBoolKeys.空调功能区故障.IfContanin(b => AirConditionFault = b);
            InBoolKeys.辅助电源功能区故障.IfContanin(b => AssistFault = b);
            InBoolKeys.紧急广播功能区故障.IfContanin(b => EmergencyBordercastFault = b);
            InBoolKeys.门功能区故障.IfContanin(b => DoorFault = b);
            InBoolKeys.紧急对讲功能区故障.IfContanin(b => EmergenctTalkFault = b);
            InBoolKeys.制动功能区故障.IfContanin(b => BrakeFault = b);
            InBoolKeys.牵引功能区故障.IfContanin(b => TractionFault = b);
            InBoolKeys.烟火功能区故障.IfContanin(b => SmokeFault = b);
            InBoolKeys.空压机功能区故障.IfContanin(b => AirPumpFault = b);
            InBoolKeys.HSCB功能区故障.IfContanin(b => HSCBFault = b);
        }

        public override void Changed(object sender, CommunicationDataChangedArgs<float> args)
        {
            base.Changed(sender, args);

            var index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.当前站];
            if (args.NewValue.ContainsKey(index))
            {
                CurrentStation = StationManager.Instance.GetStation((int)args.NewValue[index]);
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.下一站];
            if (args.NewValue.ContainsKey(index))
            {
                NextStation = StationManager.Instance.GetStation((int)args.NewValue[index]);
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.终点站];
            if (args.NewValue.ContainsKey(index))
            {
                EndStation = StationManager.Instance.GetStation((int)args.NewValue[index]);
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.网压];
            if (args.NewValue.ContainsKey(index))
            {
                NetVoltage = args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.牵引百分比];
            if (args.NewValue.ContainsKey(index))
            {
                Traction = args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.制动百分比];
            if (args.NewValue.ContainsKey(index))
            {
                Brake = args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.运行方向];
            if (args.NewValue.ContainsKey(index))
            {
                RunOrientation = (Orientation)args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.运行模式];
            if (args.NewValue.ContainsKey(index))
            {
                RunModel = (RunModel)args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.运行工况];
            if (args.NewValue.ContainsKey(index))
            {
                RunWork = (RunWork)args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.限速];
            if (args.NewValue.ContainsKey(index))
            {
                LimitSpeed = args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.速度];
            if (args.NewValue.ContainsKey(index))
            {
                Speed = args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.主风压];
            if (args.NewValue.ContainsKey(index))
            {
                MasterFanVoltage = args.NewValue[index];
            }
            index = GlobalParam.Instance.IndexConfig.InFloatDescriptionDictionary[InFloatKeys.广播模式];
            if (args.NewValue.ContainsKey(index))
            {
                BorderCastModel = (BorderCastModel)args.NewValue[index];
            }
        }
    }
}
