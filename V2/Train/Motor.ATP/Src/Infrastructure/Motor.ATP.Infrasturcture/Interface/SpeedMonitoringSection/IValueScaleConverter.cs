using System;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IValueScaleConverter
    {
        /// <summary>
        /// 
        /// </summary>
        double MinValue { get; }

        /// <summary>
        /// 
        /// </summary>
        double MaxValue { get; }

        /// <summary>
        /// 
        /// </summary>
        double Range { get; }

        /// <summary>
        /// 
        /// </summary>
        double StartScal { get; }

        /// <summary>
        /// 
        /// </summary>
        double RangePercent { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool InRange(double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double ConvertToScale(double value);
    }

    /// <summary>
    /// 
    /// </summary>
    public class LineValueLineScaleConverter : IValueScaleConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="startScal"></param>
        /// <param name="rangePercent"></param>
        public LineValueLineScaleConverter(double minValue, double maxValue, double startScal, double rangePercent)
        {
            RangePercent = rangePercent;
            StartScal = startScal;
            MaxValue = maxValue;
            MinValue = minValue;
            Range = maxValue - minValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public double MinValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double MaxValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double Range { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InRange(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public double StartScal { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double RangePercent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double ConvertToScale(double value)
        {
            return (value - MinValue) / Range * RangePercent + StartScal;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LineValueLogarithmScalConverter : IValueScaleConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="baseNumber"></param>
        /// <param name="startScal"></param>
        /// <param name="rangePercent"></param>
        public LineValueLogarithmScalConverter(double minValue, double maxValue, double baseNumber, double startScal, double rangePercent)
        {
            RangePercent = rangePercent;
            StartScal = startScal;
            BaseNumber = baseNumber;
            MaxValue = maxValue;
            MinValue = minValue;

            StartLogValue = Math.Abs(minValue) > double.Epsilon ? Math.Log(minValue, baseNumber) : 0;

            Range = Math.Log(maxValue, baseNumber) - StartLogValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public double StartLogValue { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public double MinValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double MaxValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double BaseNumber { private set; get; }

        /// <summary>
        /// 
        /// </summary>
        public double Range { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InRange(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public double StartScal { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double RangePercent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double ConvertToScale(double value)
        {
            if (Math.Abs(value) < double.Epsilon)
            {
                return 0;
            }
            //小于 2 时, 线性处理.
            if (value < 2)
            {
                return value / 2 * ((Math.Log(2, BaseNumber) - StartLogValue) / Range * RangePercent + StartScal);
            }

            return (Math.Log(value, BaseNumber) - StartLogValue) / Range * RangePercent + StartScal;
        }
    }
}