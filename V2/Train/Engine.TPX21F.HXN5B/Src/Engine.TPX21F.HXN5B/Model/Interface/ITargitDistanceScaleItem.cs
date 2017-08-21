using System.Diagnostics;
using System.Windows.Media;

namespace Engine.TPX21F.HXN5B.Model.Interface
{
    public interface ITargitDistanceScaleItem
    {
        /// <summary>
        /// 刻度值
        /// </summary>
        double Value { get; }

        /// <summary>
        /// 刻度长度， 比例值
        /// </summary>
        double DegreeLength { get; }

        /// <summary>
        /// 刻度位置 ， 比例值
        /// </summary>
        double DegreeLocation { get; }


        Pen DegreePen { get; }

        /// <summary>
        /// 显示文本
        /// </summary>
        string Text { get; }

    }

    [DebuggerDisplay("Value={Value}, DegreeLength={DegreeLength}, Text={Text}, DegreeLocation={DegreeLocation}")]
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

        public Pen DegreePen { get; private set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; private set; }


        [DebuggerStepThrough]
        public TargitDistanceScaleItem(double value, double degreeLocation, Pen degreePen, double degreeLength = 1,
            string text = null)
        {
            DegreePen = degreePen;
            Text = text;
            DegreeLocation = degreeLocation;
            DegreeLength = degreeLength;
            Value = value;
        }
    }
}