using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace Urban.HeFei.TMS.Common
{
    /// <summary>
    /// LocomotiveFailures
    /// </summary>
    public class LocomotiveFailures
    {
        /// <summary>
        /// List<LocomotiveFailure>
        /// </summary>
        public List<LocomotiveFailure> LocomativeFailureList = new List<LocomotiveFailure>();

        public LocomotiveFailures() { }
    }


    /// <summary>
    /// LocomotiveFailure
    /// </summary>
    public class LocomotiveFailure
    {
        /// <summary>
        /// LFCode
        /// </summary>
        public string LfCode
        {
            get { return m_LfCode; }
            set { m_LfCode = value; }
        }
        private string m_LfCode = string.Empty;

        /// <summary>
        /// LFIndex
        /// </summary>
        public int LfIndex
        {
            get { return m_LfIndex; }
            set { m_LfIndex = value; }
        }
        private int m_LfIndex = -1;

        /// <summary>
        /// LFName
        /// </summary>
        public string LfName
        {
            get { return m_LfName; }
            set { m_LfName = value; }
        }
        private string m_LfName = string.Empty;

        /// <summary>
        /// LFLevel
        /// </summary>
        public string LfLevel
        {
            get { return m_LfLevel; }
            set { m_LfLevel = value; }
        }
        private string m_LfLevel = string.Empty;

        /// <summary>
        /// ImageSourceStr
        /// </summary>
        public string ImageSourceStr
        {
            get { return m_ImageSourceStr; }
            set { m_ImageSourceStr = value; }
        }
        private string m_ImageSourceStr = string.Empty;

    }

    /// <summary>
    /// LocomotiveFailureHelper
    /// </summary>
    public class LocomotiveFailureHelper
    {
        /// <summary>
        /// xml序列化LocomotiveFailures
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
                AppLog.Error(e.ToString());
            }
        }

        /// <summary>
        /// xml反序列化LocomotiveFailures
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
                AppLog.Error(e.ToString());
                return null;
            }
        }
    }
}
