using System;

namespace Motor.ATP.Domain.Interface.SpeedMonitoringSection
{
    public interface IValueScaleConverter
    {
        double MinValue { get; }

        double MaxValue { get; }

        double Range { get; }

        double StartScal { get; }

        double RangePercent { get; }

        bool InRange(double value);

        double ConvertToScale(double value);
    }

    public class LineValueLineScaleConverter : IValueScaleConverter
    {
        public LineValueLineScaleConverter(double minValue, double maxValue, double startScal, double rangePercent)
        {
            RangePercent = rangePercent;
            StartScal = startScal;
            MaxValue = maxValue;
            MinValue = minValue;
            Range = maxValue - minValue;
        }

        public double MinValue { get; private set; }

        public double MaxValue { get; private set; }

        public double Range { get; private set; }

        public bool InRange(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        public double StartScal { get; private set; }

        public double RangePercent { get; private set; }

        public double ConvertToScale(double value)
        {
            return (value - MinValue) / Range * RangePercent + StartScal;
        }
    }

    public class LineValueLogarithmScalConverter : IValueScaleConverter
    {
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

        public double StartLogValue { private set; get; }

        public double MinValue { get; private set; }

        public double MaxValue { get; private set; }

        public double BaseNumber { private set; get; }

        public double Range { get; private set; }

        public bool InRange(double value)
        {
            return value >= MinValue && value <= MaxValue;
        }

        public double StartScal { get; private set; }

        public double RangePercent { get; private set; }

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