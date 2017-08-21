using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CommonUtil.Util;
using MMI.Facility.DataType.Data.Station;
using MMI.Facility.DataType.Data.TimeTable;

namespace MMI.Facility.DataType.Course
{
    [XmlRoot("时刻表")]
    public class TimeTableParamter
    {
        [XmlArray("站点")]
        [XmlArrayItem("车站")]
        public List<TimeTableItem> TiemTable { get; set; }

        private static void Test()
        {
            var t = new TimeTableParamter();
            t.TiemTable = new List<TimeTableItem>() { new TimeTableItem() { ID = "110012", ArriveTime = "22222", DepartTime = "54566" }, new TimeTableItem() { ID = "110012", ArriveTime = "22222", DepartTime = "54566" } };

            DataSerialization.SerializeToXmlFile(t, "c:\\Tools\\sta.xml");
        }

        private static List<byte> AllBytes = new List<byte>();
        public static void AddByte(byte[] date)
        {
            AllBytes.AddRange(date);
        }

        public static void ClearData()
        {
            AllBytes.Clear();
        }
        public static TimeTableParamter Initlization()
        {
            var btye = AllBytes.ToArray();
            TimeTableParamter result;
            try
            {
                 result = DataSerialization.DeserializeFromStream<TimeTableParamter>(new MemoryStream(btye, 0, btye.Length));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

            return result;
        }
    }
}
