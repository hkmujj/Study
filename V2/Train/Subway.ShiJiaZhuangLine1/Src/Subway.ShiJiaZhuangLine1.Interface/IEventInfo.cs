using System;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public interface IEventInfo : IComparable<IEventInfo>, ICloneable
    {
        /// <summary>
        /// 故障关联逻辑号
        /// </summary>
        int LogicId { get; }
        /// <summary>
        /// 序号
        /// </summary>
        int Number { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        string FaultCode { get; }
        /// <summary>
        /// 等级
        /// </summary>
        EventLevel Level { get; }
        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }
        /// <summary>
        /// 归属系统
        /// </summary>
        string System { get; }
        /// <summary>
        /// 车辆编号
        /// </summary>
        string CarNumber { get; }
        /// <summary>
        /// 是否确认
        /// </summary>
        bool IsCofirm { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        DateTime HappenDateTime { get; set; }
        /// <summary>
        /// 确认时间
        /// </summary>
        DateTime CofirmDateTime { get; set; }
        /// <summary>
        /// 故障应急处理指南
        /// </summary>
        string Handbook { get; }
        /// <summary>
        /// 检修指引
        /// </summary>
        string Guideline { get; }

    }
}