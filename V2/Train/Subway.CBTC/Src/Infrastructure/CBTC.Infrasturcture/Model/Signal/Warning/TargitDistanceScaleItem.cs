using System.Diagnostics;

namespace CBTC.Infrasturcture.Model.Signal.Warning
{
    /// <summary>
    /// 目标距离 
    /// </summary>
    public class TargitDistanceScaleItem 
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
        /// 显示文本
        /// </summary>
        public string Text { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="degreeLocation"></param>
        /// <param name="degreeLength"></param>
        /// <param name="text"></param>
        [DebuggerStepThrough]
        public TargitDistanceScaleItem(double value, double degreeLocation, double degreeLength = 1, string text = null)
        {
            
            Text = text;
            DegreeLocation = degreeLocation;
            DegreeLength = degreeLength;
            Value = value;
        }
    }
}