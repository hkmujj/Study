using System;
using System.ComponentModel;
using Motor.HMI.CRH380D.Models.State;

namespace Motor.HMI.CRH380D.Interfaces
{
    /// <summary>
    /// 事件信息
    /// </summary>
    public interface IEventInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// 显示编号
        /// </summary>
        int? Number { get; set; }
        /// <summary>
        /// 逻辑号
        /// </summary>
        int Index { get; }
        /// <summary>
        /// 所属系统
        /// </summary>
        EventSystemState System { get; } 
        /// <summary>
        /// 车厢号
        /// </summary>
        int CarNum { get; }
        /// <summary>
        /// 故障代码
        /// </summary>
        string FaultCode { get; }
        /// <summary>
        /// 单元
        /// </summary>
        int UnitNum { get; }
        /// <summary>
        /// 等级
        /// </summary>
        EventLevel Level { get; }
        /// <summary>
        /// 是否选择
        /// </summary>
        bool IsChecked { get; set; }
        /// <summary>
        /// 是否确认
        /// </summary>
        bool IsConfirm { get; set; }
        /// <summary>
        /// 故障内容
        /// </summary>
        string Describe { get; }
        /// <summary>
        /// 发生时间
        /// </summary>
        DateTime? HappenTime { get; set; }
        /// <summary>
        /// 故障帮助信息
        /// </summary>
        string HelpInfo { get; set; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEventInfo Clone<T>() where T : IEventInfo;
    }
}
