using System.Diagnostics;
using System.Drawing;

namespace Motor.ATP.Infrasturcture.Interface.TargetDistance
{
    /// <summary>
    /// 目标距离 
    /// </summary>
    public class TargitDistanceScaleItem : ITargitDistanceScaleItem
    {
        /// <summary>
        /// 刻度值
        /// </summary>
        public double Value { set; get; }

        /// <summary>
        /// 刻度长度， 比例值
        /// </summary>
        public double DegreeLength { set; get; }

        /// <summary>
        /// 刻度位置 ， 比例值
        /// </summary>
        public double DegreeLocation { set; get; }

        /// <summary>
        /// 刻度 Pen
        /// </summary>
        public Pen DegreePen { get; private set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="degreeLocation"></param>
        /// <param name="degreePen"></param>
        /// <param name="degreeLength"></param>
        /// <param name="text"></param>
        [DebuggerStepThrough]
        public TargitDistanceScaleItem(double value, double degreeLocation, Pen degreePen, double degreeLength = 1, string text = null)
        {
            DegreePen = degreePen;
            Text = text;
            DegreeLocation = degreeLocation;
            DegreeLength = degreeLength;
            Value = value;
        }
    }
}