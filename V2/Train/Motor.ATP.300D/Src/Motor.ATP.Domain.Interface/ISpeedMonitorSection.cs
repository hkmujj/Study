using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 速度监视
    /// </summary>
    public interface ISpeedMonitoringSection : IATPPartial
    {
        /// <summary>
        /// 监视类型
        /// </summary>
        SpeedMonitoringSectionType Type { get; }

        ISpeedProfile SpeedProfile { get; }

        /// <summary>
        /// 起模点, 制动曲线区域起始点
        /// </summary>
        double BrakingStartPoint { get; }
    }

    /// <summary>
    /// 速度曲线
    /// </summary>
    public interface ISpeedProfile 
    {
        /// <summary>
        /// 描述曲线的点
        /// </summary>
        List<Tuple<double, double>> PointCollection { get; }
    }


    /// <summary>
    /// 监视类型
    /// </summary>
    public enum SpeedMonitoringSectionType
    {
        /// <summary>
        /// 没有监视
        /// </summary>
        None = 0,

        /// <summary>
        /// ceiling speed monitor section
        /// </summary>
        CSM,

        /// <summary>
        /// target speed monitor section
        /// </summary>
        TSM,

        /// <summary>
        /// Relase speed monitoring 开口速度监控
        /// </summary>
        RSM,
    }
}
