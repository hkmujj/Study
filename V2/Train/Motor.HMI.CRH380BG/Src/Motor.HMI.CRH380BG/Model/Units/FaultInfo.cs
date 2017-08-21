using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Excel.Interface;

namespace Motor.HMI.CRH380BG.Model.Units
{
    /// <summary>
    /// 故障信息
    /// </summary>
    [ExcelLocation("Motor.HMI.CRH380BG故障信息.xls", "Sheet1")]
    public class FaultInfo : NotificationObject, IFaultInfo, ISetValueProvider
    {
        static FaultInfo()
        {
            Empty = new FaultInfo();
        }
        /// <summary>
        /// 空
        /// </summary>
        public static readonly FaultInfo Empty;

        private bool m_IsChecked;

       

        /// <summary>
        /// 逻辑号
        /// </summary>
        [ExcelField("逻辑号")]
        public int Index { get; set; }

        /// <summary>
        /// 车厢号
        /// </summary>
        public int CarNumber { get; set; }
        /// <summary>
        /// 故障类型
        /// </summary>
        public string FaultType { get; set; }

        /// <summary>
        /// 故障代码
        /// </summary>
        [ExcelField("故障代码")]
        public string FaultCode { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [ExcelField("故障等级")]
        public string FaultLevel { get; set; }
        [ExcelField("故障名称")]
        public string FaultName { get; set; }

        [ExcelField("故障报告")]
        public string FaultReport { get; set; }

        [ExcelField("V>0")]
        public string TrainRunningCheck { get; set; }

        [ExcelField("V=0")]
        public string TrainStopCheck { get; set; }

        [ExcelField("故障已读后发送位")]
        public string AfterFaultSend { get; set; }

        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsChecked
        {
            get { return m_IsChecked; }
            set
            {
                if (value == m_IsChecked)
                {
                    return;
                }
                m_IsChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }

        /// <summary>
        /// 是否确认
        /// </summary>
        public bool IsConfirm { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime HappenTime { get; set; }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IFaultInfo Clone<T>() where T : IFaultInfo
        {
            var t = new FaultInfo
            {
                Index = Index,
                CarNumber = CarNumber,
                FaultType = FaultType,
                FaultCode = FaultCode,
                FaultLevel = FaultLevel,
                FaultName = FaultName,
                IsConfirm = false,
            };
            return t;
        }

        /// <summary/>
        /// <param name="propertyOrFieldName"/><param name="value"/>
        public void SetValue(string propertyOrFieldName, string value)
        {
           
        }
    }
}
