using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SS4B_TMS.Common
{
    /// <summary>
    ///     StationInfos
    /// </summary>
    public class StationInfos
    {
        public List<StationInfo> StationInfoList = new List<StationInfo>();
    }

    /// <summary>
    ///     StationInfo
    /// </summary>
    public class StationInfo
    {
        /// <summary>
        ///     Index
        /// </summary>
        public int Index { get; set; } = -1;

        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; } = "";
    }

    /// <summary>
    ///     StationInfoHelper
    /// </summary>
    public static class StationInfoHelper
    {
        /// <summary>
        ///     xml序列化StationInfo
        /// </summary>
        /// <param name="modelDataConvertInfos">StationInfo</param>
        /// <param name="fileName">序列化文件名</param>
        public static void SerializeStationInfo(StationInfos stationInfos, string fileName)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create))
                {
                    var s = new XmlSerializer(typeof(StationInfos));
                    s.Serialize(stream, stationInfos);
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        ///     xml反序列化StationInfo
        /// </summary>
        /// <param name="fileName">序列化文件名</param>
        /// <returns>StationInfo</returns>
        public static StationInfos DeserializeStationInfo(string fileName)
        {
            var tempInfo = new StationInfos();
            var fs = (FileStream)null;
            try
            {
                using (fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var xs = new XmlSerializer(typeof(StationInfos));
                    tempInfo = (StationInfos)xs.Deserialize(fs);
                    return tempInfo;
                }
            }
            catch (Exception e)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }
    }
}