using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel.Interface;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Motor.HMI.CRH380BG.Model.ConfigModel
{
    [ExcelLocation("Motor.HMI.CRH380BG故障信息.xls", "Sheet1")]
    [DebuggerDisplay("Index={Index}, CarNumber={CarNumber}, FaultType={FaultType}, FaultCode={FaultCode}, FaultLevel={FaultLevel}, FaultName={FaultName}, FaultReport={FaultReport}, TrainRunningCheck={TrainRunningCheck}, TrainStopCheck={TrainStopCheck}, AfterFaultSend={AfterFaultSend}")]
    //[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class NotifyInfoConfig : ISetValueProvider
    {
        public NotifyInfoConfig()
        {

        }

        public NotifyInfoConfig(int carNumer, string faultType, string faultCode, string faultLevel, string faultName, string faultReport, string trainRun, string trainStop, string afterFaultSend, int index = -1)
        {
            Index = index;
            CarNumber = carNumer;
            FaultType = faultType;
            FaultCode = faultCode;
            FaultLevel = faultLevel;
            FaultName = faultName;
            FaultReport = faultReport;
            TrainRunningCheck = trainRun;
            TrainStopCheck = trainStop;
            AfterFaultSend = afterFaultSend;
        }


        [ExcelField("逻辑号")]
        public int Index { get; private set; }

        /// <summary>
        /// 车厢号
        /// </summary>
        [ExcelField("车厢号")]
        public int CarNumber { private set; get; }
        
        /// <summary>
        /// 故障类型
        /// </summary>
        [ExcelField("故障类型")]
        public string FaultType { private set; get; }

        /// <summary>
        /// 故障代码
        /// </summary>
        [ExcelField("故障代码")]
        public string FaultCode { private set; get; }

        /// <summary>
        /// 等级
        /// </summary>
        [ExcelField("故障等级")]
        public string FaultLevel { private set; get; }

        [ExcelField("故障名称")]
        public string FaultName { private set; get; }

        [ExcelField("故障报告")]
        public string FaultReport { private set; get; }

        [ExcelField("V大于0")]
        public string TrainRunningCheck { private set; get; }

        [ExcelField("V等于0")]
        public string TrainStopCheck { private set; get; }

        [ExcelField("故障已读后发送位")]
        public string AfterFaultSend { private set; get; }



        /// <summary>
        /// </summary>
        /// <param name="propertyOrFieldName"></param>
        /// <param name="value"></param>
        public void SetValue(string propertyOrFieldName, string value)
        {

        }
    }
}
