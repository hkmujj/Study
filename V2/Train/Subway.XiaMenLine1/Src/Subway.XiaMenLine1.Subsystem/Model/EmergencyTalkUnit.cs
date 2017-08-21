using System;
using System.Diagnostics;
using CommonUtil.Util;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Subway.XiaMenLine1.Interface.Enum;

namespace Subway.XiaMenLine1.Subsystem.Model
{
    [ExcelLocation("主运行界面紧急通话子界面内容和接口.xls", "Sheet1")]
    [DebuggerDisplay("Car={Car}, Location={Location}, State={State}")]
    public class EmergencyTalkUnit : NotificationObject, ISetValueProvider
    {
        private EmergencyTalkState m_State;

        public EmergencyTalkState State
        {
            set
            {
                if (value == m_State)
                {
                    return;
                }
                m_State = value;
                RaisePropertyChanged(() => State);
            }
            get { return m_State; }
        }

        [ExcelField("Car")]
        public int Car { private set; get; }

        [ExcelField("Location")]
        public int Location { private set; get; }

        [ExcelField("UnitFalt")]
        public string UnitFalt { private set; get; }

        [ExcelField("UnitRequest")]
        public string UnitRequest { private set; get; }

        [ExcelField("UnitActive")]
        public string UnitActive { private set; get; }

        [ExcelField("UnitNormal")]
        public string UnitNormal { private set; get; }

        public string GetIndexName(EmergencyTalkState state)
        {
            switch (state)
            {
                case EmergencyTalkState.Null:
                    break;
                case EmergencyTalkState.Normal:
                    break;
                case EmergencyTalkState.Select:
                    break;
                case EmergencyTalkState.Fault:
                    break;
                case EmergencyTalkState.UnitFalt:
                    return UnitFalt;
                case EmergencyTalkState.UnitRequest:
                    return UnitRequest;
                case EmergencyTalkState.UnitActive:
                    return UnitActive;
                case EmergencyTalkState.UnitNormal:
                    return UnitNormal;
                default:
                    return String.Empty;
            }
            return String.Empty;
        }

        public void SetValue(string propertyOrFieldName, string value)
        {
            switch (propertyOrFieldName)
            {
                case "Car":
                    Car = Convert.ToInt32(value);
                    break;
                case "Location":
                    Location = Convert.ToInt32(value);
                    break;
                case "UnitFalt":
                    UnitFalt = value;
                    break;
                case "UnitRequest":
                    UnitRequest = value;
                    break;
                case "UnitActive":
                    UnitActive = value;
                    break;
                case "UnitNormal":
                    UnitNormal = value;
                    break;
                default:
                    var error = string.Format("Can not found property of field , name={0}, value={1}",
                        propertyOrFieldName, value);
                    AppLog.Error(error);
                    throw new ArgumentOutOfRangeException(error);
            }
        }
    }
}