using System.Collections.Generic;

namespace Engine.HMI.SS3B.Interface
{
    public interface IGraduationResouce
    {
        Dictionary<double, double> Souce { get; }
        /// <summary>
        /// 获取当前值所占百分比
        /// </summary>
        /// <param name="currentValue">当前值</param>
        /// <returns></returns>
        double GetScal(double currentValue);
        /// <summary>
        /// 箭头值
        /// </summary>
        double Enmergency { get; }
        /// <summary>
        /// 长短对比
        /// </summary>
        int Contrast { get; }
        /// <summary>
        /// 线条总数
        /// </summary>
        int LineCount { get; }
        /// <summary>
        /// 最大值
        /// </summary>
        double MaxValue { get; }
        /// <summary>
        /// 长线条长度
        /// </summary>

        double LongLength { get; }
        /// <summary>
        /// 短线条长度
        /// </summary>

        double ShortLength { get; }
        /// <summary>
        /// 偏移
        /// </summary>
        double Offset { get; }
        
    }
}