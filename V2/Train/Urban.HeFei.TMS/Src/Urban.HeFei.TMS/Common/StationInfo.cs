using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Urban.HeFei.TMS.Common
{
    /// <summary>
    /// StationInfos
    /// </summary>
    public class StationInfos
    {
        public List<StationInfo> StationInfoList { set; get; }
    }

    /// <summary>
    /// StationInfo
    /// </summary>
    public class StationInfo
    {
        /// <summary>
        /// LogicIndex
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        public StationInfo()
        {
            Index = -1;
            Name = "";
        }
    }
    
}
