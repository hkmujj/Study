using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data;
using Subway.XiaMenLine1.Interface;
using Subway.XiaMenLine1.Interface.Enum;
using Subway.XiaMenLine1.Interface.Model;
using Subway.XiaMenLine1.Interface.Resouce;
using Subway.XiaMenLine1.Subsystem.Extension;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    public class MainRunningBtnViewModel : ViewModelBase, IButton
    {
        private FrsmHighSpeed m_FrsmHighSpeed;
        private SmokeStatus m_SmokeStatus;
        private TractionStatus m_TractionStatus;
        private EmergencyTalkState m_EmergencyTalkState;
        private BrakeStatus m_BrakeStatus;
        private DoorStatus m_DoorStatus;
        private AssistPowerState m_AirAssistPowerState;
        private AirPumpStatus m_AirPumpStatus;
        private AirConditionState m_AirConditionState;
        private string m_ButtonChangedStr;
        private List<IContentPage> AllContentPage;
        private string m_Button9Str;

        public MainRunningBtnViewModel(IMMI parent)
            : base(parent)
        {
            Reset();
            AllContentPage = ServiceLocator.Current.GetAllInstances<object>().Where(w => w.GetType().GetInterface("IContentPage", false) != null).Cast<IContentPage>().ToList();

        }

        public void ChangeStatus(object sender, CommunicationDataChangedArgs e)
        {
            ChangeBools(e.ChangedBools);
            ChangeFloats(e.ChangedFloats);
        }

        private void Reset()
        {
            AirConditionState = AirConditionState.Normal;
            AirAssistPowerState = AssistPowerState.Normal;
            DoorStatus = DoorStatus.NormalDisplay;
            EmergencyTalkState = EmergencyTalkState.Normal;
            BrakeStatus = BrakeStatus.Normal;
            SmokeStatus = SmokeStatus.Normal;
            AirPumpStatus = AirPumpStatus.Normal;
            TractionStatus = TractionStatus.Normal;
            FrsmHighSpeed = FrsmHighSpeed.Normal;
        }

        public void ResetButtonState()
        {
            if (AirConditionState != AirConditionState.Fault)
            {
                AirConditionState = AirConditionState.Normal;
            }
            if (AirAssistPowerState != AssistPowerState.Fault)
            {
                AirAssistPowerState = AssistPowerState.Normal;
            }
            if (DoorStatus != DoorStatus.FaultDispaly && DoorStatus != DoorStatus.CutDisplay)
            {
                DoorStatus = DoorStatus.NormalDisplay;
            }
            if (EmergencyTalkState != EmergencyTalkState.Fault)
            {
                EmergencyTalkState = EmergencyTalkState.Normal;
            }
            if (BrakeStatus != BrakeStatus.BrakeFault && BrakeStatus != BrakeStatus.BrakeCut)
            {
                BrakeStatus = BrakeStatus.Normal;
            }
            if (SmokeStatus != SmokeStatus.Smoke)
            {
                SmokeStatus = SmokeStatus.Normal;
            }
            if (AirPumpStatus != AirPumpStatus.Falut)
            {
                AirPumpStatus = AirPumpStatus.Normal;
            }
            if (TractionStatus != TractionStatus.Cut && TractionStatus != TractionStatus.Falut)
            {
                TractionStatus = TractionStatus.Normal;
            }
            if (FrsmHighSpeed != FrsmHighSpeed.Fault)
            {
                FrsmHighSpeed = FrsmHighSpeed.Normal;
            }
        }

        public string Button8Str
        {
            get { return m_ButtonChangedStr; }
            set
            {
                if (value == m_ButtonChangedStr)
                {
                    return;
                }
                m_ButtonChangedStr = value;
                RaisePropertyChanged(() => Button8Str);
            }
        }

        public string Button9Str
        {
            get { return m_Button9Str; }
            set
            {
                if (value == m_Button9Str)
                    return;

                m_Button9Str = value;
                RaisePropertyChanged(() => Button9Str);
            }
        }


        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {
            args.UpdateIfContains(InBoolKeys.Inb空调系统功能区故障, () => { AirConditionState = AirConditionState.Fault; },
                () =>
                {
                    AirConditionState = AllContentPage[0].IsFouce ? AirConditionState.Select : AirConditionState.Normal;
                });
            args.UpdateIfContains(InBoolKeys.Inb辅助电源功能区故障, () => AirAssistPowerState = AssistPowerState.Fault, (() =>
             {
                 AirAssistPowerState = AllContentPage[2].IsFouce ? AssistPowerState.Select : AssistPowerState.Normal;
             }));
            args.UpdateIfContains(InBoolKeys.Inb门状态功能区故障显示, () => DoorStatus = DoorStatus.FaultDispaly, () =>
            {
                DoorStatus = AllContentPage[4].IsFouce ? DoorStatus.SelectDisplay : DoorStatus.NormalDisplay;
            });
            args.UpdateIfContains(InBoolKeys.Inb门状态功能区切除显示, () => DoorStatus = DoorStatus.CutDisplay, () =>
            {
                DoorStatus = AllContentPage[4].IsFouce ? DoorStatus.SelectDisplay : DoorStatus.NormalDisplay;
            });
            args.UpdateIfContains(InBoolKeys.Inb紧急对讲功能区故障显示, () => EmergencyTalkState = EmergencyTalkState.Fault,
                () =>
                {
                    EmergencyTalkState = AllContentPage[5].IsFouce ? EmergencyTalkState.Select : EmergencyTalkState.Normal;
                });
            args.UpdateIfContains(InBoolKeys.Inb制动状态功能区故障显示, () => BrakeStatus = BrakeStatus.BrakeFault, () =>
            {
                BrakeStatus = AllContentPage[3].IsFouce ? BrakeStatus.Select : BrakeStatus.Normal;
            });
            args.UpdateIfContains(InBoolKeys.Inb制动状态功能区切除显示, () => BrakeStatus = BrakeStatus.BrakeCut, () =>
            {
                BrakeStatus = AllContentPage[3].IsFouce ? BrakeStatus.Select : BrakeStatus.Normal;
            });
            args.UpdateIfContains(InBoolKeys.Inb轴温探测功能区检测到烟温, () => SmokeStatus = SmokeStatus.Smoke, () =>
            {
                SmokeStatus = AllContentPage[7].IsFouce ? SmokeStatus.Selct : SmokeStatus.Normal;
            });
            args.UpdateIfContains(InBoolKeys.Inb空压机功能区故障显示, () => AirPumpStatus = AirPumpStatus.Falut, (() =>
             {
                 AirPumpStatus = AllContentPage[1].IsFouce ? AirPumpStatus.Select : AirPumpStatus.Normal;
             }));
            args.UpdateIfContains(InBoolKeys.Inb受电弓功能区故障显示, () => FrsmHighSpeed = FrsmHighSpeed.Fault, () =>
            {
                FrsmHighSpeed = AllContentPage[6].IsFouce ? FrsmHighSpeed.Select : FrsmHighSpeed.Normal;
            });

            args.UpdateIfContains(InBoolKeys.Inb牵引状态功能区故障显示, () => { TractionStatus = TractionStatus.Falut; }, () =>
                  {
                      TractionStatus = AllContentPage[8].IsFouce ? TractionStatus.Select : TractionStatus.Normal;
                  });
            args.UpdateIfContains(InBoolKeys.Inb牵引状态功能区切除显示, () => { TractionStatus = TractionStatus.Cut; }, () =>
            {
                TractionStatus = AllContentPage[8].IsFouce ? TractionStatus.Select : TractionStatus.Normal;
            });
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }

        public AirConditionState AirConditionState
        {
            get { return m_AirConditionState; }
            set
            {
                if (value == m_AirConditionState)
                {
                    return;
                }
                m_AirConditionState = value;
                RaisePropertyChanged(() => AirConditionState);
            }
        }

        public AirPumpStatus AirPumpStatus
        {
            get { return m_AirPumpStatus; }
            set
            {
                if (value == m_AirPumpStatus)
                {
                    return;
                }
                m_AirPumpStatus = value;
                RaisePropertyChanged(() => AirPumpStatus);
            }
        }

        public AssistPowerState AirAssistPowerState
        {
            get { return m_AirAssistPowerState; }
            set
            {
                if (value == m_AirAssistPowerState)
                {
                    return;
                }
                m_AirAssistPowerState = value;
                RaisePropertyChanged(() => AirAssistPowerState);
            }
        }

        public DoorStatus DoorStatus
        {
            get { return m_DoorStatus; }
            set
            {
                if (value == m_DoorStatus)
                {
                    return;
                }
                m_DoorStatus = value;
                RaisePropertyChanged(() => DoorStatus);
            }
        }

        public BrakeStatus BrakeStatus
        {
            get { return m_BrakeStatus; }
            set
            {
                if (value == m_BrakeStatus)
                {
                    return;
                }
                m_BrakeStatus = value;
                RaisePropertyChanged(() => BrakeStatus);
            }
        }

        public EmergencyTalkState EmergencyTalkState
        {
            get { return m_EmergencyTalkState; }
            set
            {
                if (value == m_EmergencyTalkState)
                {
                    return;
                }
                m_EmergencyTalkState = value;
                RaisePropertyChanged(() => EmergencyTalkState);
            }
        }

        public TractionStatus TractionStatus
        {
            get { return m_TractionStatus; }
            set
            {
                if (value == m_TractionStatus)
                {
                    return;
                }
                m_TractionStatus = value;
                RaisePropertyChanged(() => TractionStatus);
            }
        }

        public SmokeStatus SmokeStatus
        {
            get { return m_SmokeStatus; }
            set
            {
                if (value == m_SmokeStatus)
                {
                    return;
                }
                m_SmokeStatus = value;
                RaisePropertyChanged(() => SmokeStatus);
            }
        }

        public FrsmHighSpeed FrsmHighSpeed
        {
            get { return m_FrsmHighSpeed; }
            set
            {
                if (value == m_FrsmHighSpeed)
                {
                    return;
                }
                m_FrsmHighSpeed = value;
                RaisePropertyChanged(() => FrsmHighSpeed);
            }
        }

    }
}