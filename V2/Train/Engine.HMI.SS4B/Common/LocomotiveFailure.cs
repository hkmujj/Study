using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SS4B_TMS.Common
{
    /// <summary>
    ///     LocomotiveFailures
    /// </summary>
    public class LocomotiveFailures
    {
        /// <summary>
        ///     List<LocomotiveFailure>
        /// </summary>
        public List<LocomotiveFailure> LocomativeFailureList = new List<LocomotiveFailure>();
    }

    /// <summary>
    ///     LocomotiveFailure
    /// </summary>
    public class LocomotiveFailure
    {
        /// <summary>
        ///     LFCode
        /// </summary>
        public string LfCode { get; set; } = string.Empty;

        /// <summary>
        ///     LFIndex
        /// </summary>
        public int LfIndex { get; set; } = -1;

        /// <summary>
        ///     LFName
        /// </summary>
        public string LfName { get; set; } = string.Empty;

        /// <summary>
        ///     LFLevel
        /// </summary>
        public string LfLevel { get; set; } = string.Empty;

        /// <summary>
        ///     ImageSourceStr
        /// </summary>
        public string ImageSourceStr { get; set; } = string.Empty;
    }

    /// <summary>
    ///     LocomotiveFailureHelper
    /// </summary>
    public class LocomotiveFailureHelper
    {
        /// <summary>
        ///     xml序列化LocomotiveFailures
        /// </summary>
        /// <param name="modelDataConvertInfos">LocomotiveFailures</param>
        /// <param name="fileName">序列化文件名</param>
        public static void SerializeLocomotiveFailures(LocomotiveFailures locomotiveFailures, string fileName)
        {
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create))
                {
                    var s = new XmlSerializer(typeof(LocomotiveFailures));
                    s.Serialize(stream, locomotiveFailures);
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        ///     xml反序列化LocomotiveFailures
        /// </summary>
        /// <param name="fileName">序列化文件名</param>
        /// <returns>LocomotiveFailures</returns>
        public static LocomotiveFailures DeserializeLocomotiveFailures(string fileName)
        {
            var tempInfo = new LocomotiveFailures();
            var fs = (FileStream)null;
            try
            {
                using (fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var xs = new XmlSerializer(typeof(LocomotiveFailures));
                    tempInfo = (LocomotiveFailures)xs.Deserialize(fs);
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