using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
// ReSharper disable All

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot]
    public enum BNetPortDefine
    {
        /// <summary>
        /// 
        /// </summary>
        Port_1000,
        /// <summary>
        /// 
        /// </summary>
        Port_2000,
        /// <summary>
        /// 
        /// </summary>
        Port_3000,
        /// <summary>
        /// 
        /// </summary>
        Port_4000,
    }

    /// <summary>
    /// 
    /// </summary>
    public class BNetPortDefineHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<BNetPortDefine> AllBNetPortCollection { private set; get; }

        static BNetPortDefineHelper()
        {
            AllBNetPortCollection = Enum.GetValues(typeof (BNetPortDefine)).Cast<BNetPortDefine>().ToList();
        }
    }
}