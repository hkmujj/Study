using System.Collections.Generic;
using Engine.HMI.SS3B.Interface;

namespace Engine.HMI.SS3B.View.Model
{
    public class LineResouceOne : IGraduationResouce
    {
        public Dictionary<double, double> Souce { get; set; }
        public virtual double GetScal(double currentValue)
        {
            return (double)currentValue / (double)MaxValue;
        }

        /// <summary>
        /// 箭头值
        /// </summary>
        public double Enmergency { get; set; }

        /// <summary>
        /// 长短对比
        /// </summary>
        public int Contrast { get; set; }

        /// <summary>
        /// 线条总数
        /// </summary>
        public int LineCount { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// 长线条长度
        /// </summary>
        public double LongLength { get; set; }

        /// <summary>
        /// 短线条长度
        /// </summary>
        public double ShortLength { get; set; }


        /// <summary>
        /// 偏移
        /// </summary>
        public double Offset { get; set; }
    }
}