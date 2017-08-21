using System;
using System.Xml.Serialization;
using CRH2MMI.Config.ConfigModel;

namespace CRH2MMI.Common.Models
{
    [Serializable]
    public class MatrixViewConfigItem : CRH2CommunicationPortModel
    {
        /// <summary>
        /// 车厢号
        /// </summary>
        [XmlAttribute]
        public int CarNo { set; get; }

    }
}