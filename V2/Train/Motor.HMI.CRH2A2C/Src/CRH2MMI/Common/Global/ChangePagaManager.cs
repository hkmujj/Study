using System;
using CRH2MMI.Common.Config;
using CRH2MMI.Fault;
using CRH2MMI.TelentControl;

namespace CRH2MMI.Common.Global
{
    class ChangePagaManager
    {
        public static ChangePagaManager Instance { private set; get; }

        static ChangePagaManager()
        {
            Instance = new ChangePagaManager();
        }

        private ChangePagaManager()
        {

        }

        private FaultMgr.IFaultGetter m_AllFaultGetter;

        private string m_LastError;

        public void Initalize()
        {
            m_AllFaultGetter = FaultMgr.Instance.GetterFacotry.CreateGetter(FaultGetterType.All);
        }

        public string GetLastError()
        {
            return m_LastError;
        }

        public bool CanChangeTo(ViewConfig page)
        {
            m_LastError = string.Empty;
            switch (page)
            {
                case ViewConfig.Black:
                    break;
                case ViewConfig.InitView:
                    break;
                case ViewConfig.DriveState:
                    break;
                case ViewConfig.CarInfo:
                    break;
                case ViewConfig.RemovalState:
                    break;
                case ViewConfig.BrakeInfo:
                    break;
                case ViewConfig.Tow1:
                    break;
                case ViewConfig.PowerVoltage:
                    break;
                case ViewConfig.DoorInfo:
                    break;
                case ViewConfig.Racing:
                    break;
                case ViewConfig.Tow2:
                    break;
                case ViewConfig.AcumuElec:
                    break;
                case ViewConfig.Axis:
                    break;
                case ViewConfig.Telecontr:
                    if (Telecontr.SelfLocked)
                    {
                        m_LastError = "其它界面正在显示远程控制切除。";
                        return false;
                    }
                    break;
                case ViewConfig.BreakLocked:
                    break;
                case ViewConfig.TNSet:
                    break;
                case ViewConfig.MonitorSet:
                    break;
                case ViewConfig.JackInfo:
                    break;
                case ViewConfig.CurrentStationSet:
                    break;
                case ViewConfig.TNChange:
                    break;
                case ViewConfig.Marsh:
                    break;
                case ViewConfig.FaultView:
                    if (!m_AllFaultGetter.HasFault)
                    {
                        m_LastError = "无故障记录。";
                        return false;
                    }
                    break;
                case ViewConfig.Delivery:
                    break;
                case ViewConfig.PowerClassfy:
                    break;
                case ViewConfig.Trans:
                    break;
                case ViewConfig.Notify:
                    break;
                case ViewConfig.FaultInfo:
                    break;
                case ViewConfig.Preoperation:
                    break;
                case ViewConfig.VigilantView:
                    break;
                case ViewConfig.BPRescueView:
                    break;
                case ViewConfig.AxleTemperature:
                    break;
                case ViewConfig.ConnetctHoodInfo:
                    break;
                case ViewConfig.Bp:
                    break;
                case ViewConfig.Screensavers:
                    break;
                case ViewConfig.SprinkleSand:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("page");
            }

            return true;
        }
    }
}
