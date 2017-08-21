using System;
using System.ComponentModel;

namespace LightRail.HMI.GZYGDC.Interface
{
    /// <summary>
    /// 事件信息
    /// </summary>
    public interface IEventInfo:INotifyPropertyChanged
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
        /// 故障代码
        /// </summary>
        string FaultCode { get; }
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
        /// 描述
        /// </summary>
        string Describe { get; }
        /// <summary>
        /// 发生时间
        /// </summary>
        DateTime? HappenTime { get; set; }
        /// <summary>
        /// 操作提示
        /// </summary>
        string OperationTrips { get; set; }
        /// <summary>
        /// 检修指引
        /// </summary>
        string CheckTrips { get; set; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEventInfo Clone<T>() where T : IEventInfo;
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EventLevel
    {
        /// <summary>
        /// 默认所有
        /// </summary>
        //[Description("默认所有")]
        Normal,
        /// <summary>
        /// 提示
        /// </summary>
        [Description("提示")]
        Tip = 1,
        /// <summary>
        /// 轻微
        /// </summary>
        [Description("轻微")]
        Light = 2,
        /// <summary>
        /// 中等
        /// </summary>
        [Description("中等")]
        Medium = 3,
        /// <summary>
        /// 严重
        /// </summary>
        [Description("严重")]
        Grave = 4,
    }
}