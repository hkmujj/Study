using System;

namespace HD_HXD2_TMS.Common
{
    public enum FalutGrade
    {
        A = 0,
        B = 1,
        C = 2
    }

    public enum Direction
    {
        零位 = 0,
        向前 = 1,
        向后 = 2
    }

    public class FalutInfo
    {
        public Int32 LogicID { get; set; }

        /// <summary>
        /// A B
        /// </summary>
        public String TrainName { get; set; }

        public Int32 TrainID { get; set; }

        public String Code { get; set; }

        public String Name { get; set; }

        public FalutGrade Grade { get; set; }

        public String Point { get; set; }

        public String PointOut { get; set; }

        public String StartTime { get; set; }

        public float Speed { get; set; }

        public float Voltage { get; set; }

        public float Electric { get; set; }

        public Direction Direction { get; set; }

        public float Level { get; set; }

        public Boolean IsOK { get; set; }

        private Boolean _oldValue = false;

        public FalutInfo(Int32 logicID,String trainName,Int32 trainID,String code,String name,String grade,String point,String pointOut)
        {
            LogicID = logicID;
            TrainName = trainName;
            TrainID = trainID;
            Code = code;
            Name = name;
            Grade = (FalutGrade) Enum.Parse(typeof (FalutGrade), grade);
            Point = point;
            PointOut = pointOut;
        }

        public FalutInfo(FalutInfo fi)
        {
            LogicID = fi.LogicID;
            TrainName = fi.TrainName;
            TrainID = fi.TrainID;
            Code = fi.Code;
            Name = fi.Name;
            Grade = fi.Grade;
            Point = fi.Point;
            PointOut = fi.PointOut;
            StartTime = fi.StartTime;
            Speed = fi.Speed;
            Voltage = fi.Voltage;
            Electric = fi.Electric;
            Direction = fi.Direction;
            Level = fi.Level;
        }

        public Boolean GetValue(Boolean value)
        {
            Boolean returnValue = (_oldValue != value);
            _oldValue = value;

            return returnValue;
        }
    }
}
