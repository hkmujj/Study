using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Motor.HMI.CRH380BG.Model.Interface
{
    /// <summary>
    /// 故障信息
    /// </summary>
    public interface IFaultInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// 逻辑号
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 车厢号
        /// </summary>
        int CarNumber { get; set; }
        /// <summary>
        /// 故障类型
        /// </summary>
        string FaultType { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        string FaultCode { get; set; }
        /// <summary>
        /// 故障等级
        /// </summary>
        string FaultLevel { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        string FaultName { get; set; }
        /// <summary>
        /// 故障报告
        /// </summary>
        string FaultReport { get; set; }
        /// <summary>
        /// V>0
        /// </summary>
        string TrainRunningCheck { get; set; }
        /// <summary>
        /// V<0
        /// </summary>
        string TrainStopCheck { get; set; }
        /// <summary>
        /// 故障已读后发送位
        /// </summary>
        string AfterFaultSend { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        bool IsChecked { get; set; }
        /// <summary>
        /// 是否确认
        /// </summary>
        bool IsConfirm { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        DateTime HappenTime { get; set; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFaultInfo Clone<T>() where T : IFaultInfo;
    }
}
