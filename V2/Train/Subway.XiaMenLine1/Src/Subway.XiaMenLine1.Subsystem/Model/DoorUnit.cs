using System;
using System.Diagnostics;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [ExcelLocation("主运行界面门子界面内容和接口.xls", "Sheet1")]
    [DebuggerDisplay("Car={Car}, DoorIndex={DoorIndex}, DoorStatus={DoorStatus}")]
    public class DoorUnit : NotificationObject, ISetValueProvider
    {
        private DoorStatus m_DoorStatus;

        public DoorStatus DoorStatus
        {
            set
            {
                if (value == m_DoorStatus)
                {
                    return;
                }
                m_DoorStatus = value;
                RaisePropertyChanged(() => DoorStatus);
            }
            get { return m_DoorStatus; }
        }

        [ExcelField("Car")]
        public int Car { set; get; }

        [ExcelField("DoorIndex")]
        public int DoorIndex { set; get; }

        //public string Null { set; get; }
        //public string NormalDisplay { set; get; }
        //public string SelectDisplay { set; get; }
        //public string CutDisplay { set; get; }
        //public string FaultDispaly { set; get; }
        [ExcelField("EmeregencyUnlock")]
        public string EmeregencyUnlock { set; get; }

        [ExcelField("Cut")]
        public string Cut { set; get; }

        [ExcelField("Fault")]
        public string Fault { set; get; }

        [ExcelField("Check")]
        public string Check { set; get; }
        [ExcelField("OpenCheck")]
        public string OpenCheck { set; get; }
        //public string Flick { set; get; }
        [ExcelField("Closed")]
        public string Closed { set; get; }
        [ExcelField("CloseFault")]
        public string CloseFault { get; set; }
        //public string DoorFlick { set; get; }
        //[ExcelField("DoorNormal")]
        //public string DoorNormal { set; get; }

        [ExcelField("Opened")]
        public string Opened { set; get; }

        [ExcelField("CommunicationFault")]
        public string CommunicationFault { set; get; }

        [ExcelField("StateUnkown")]
        public string StateUnkown { set; get; }

        public string GetIndexName(DoorStatus doorStatus)
        {
            switch (doorStatus)
            {
                case DoorStatus.Null:
                    return null;
                case DoorStatus.NormalDisplay:
                    return null;
                case DoorStatus.SelectDisplay:
                    return null;
                case DoorStatus.CutDisplay:
                    return null;
                case DoorStatus.FaultDispaly:
                    return null;
                case DoorStatus.EmeregencyUnlock:
                    return EmeregencyUnlock;
                case DoorStatus.Cut:
                    return Cut;
                case DoorStatus.OpenFault:
                    return Fault;
                case DoorStatus.CloseCheck:
                    return Check;
                case DoorStatus.Flick:
                    return null;
                case DoorStatus.Closed:
                    return Closed;
                case DoorStatus.DoorFlick:
                    return null;
                case DoorStatus.Opened:
                    return Opened;
                case DoorStatus.CommunicationFault:
                    return CommunicationFault;
                case DoorStatus.StateUnkown:
                    return StateUnkown;
                case DoorStatus.CloseFault:
                    return CloseFault;
                case DoorStatus.OpenCheck:
                    return OpenCheck;
                default:
                    return null;
            }
        }


        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Car":
                    Car = Convert.ToInt32(value);
                    break;
                case "DoorIndex":
                    DoorIndex = Convert.ToInt32(value);
                    break;
                case "EmeregencyUnlock":
                    EmeregencyUnlock = value;
                    break;
                case "Cut":
                    Cut = value;
                    break;
                case "OpenFault":
                    Fault = value;
                    break;
                case "CloseCheck":
                    Check = value;
                    break;
                //case "Flick":
                //    Flick = value;
                //    break;
                case "Closed":
                    Closed = value;
                    break;
                //case "DoorNormal":
                //    DoorNormal = value;
                //break;
                case "Opened":
                    Opened = value;
                    break;
                case "CommunicationFault":
                    CommunicationFault = value;
                    break;
                case "StateUnkown":
                    StateUnkown = value;
                    break;

            }
        }
    }
}