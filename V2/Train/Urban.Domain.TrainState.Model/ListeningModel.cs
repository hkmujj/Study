using System.Diagnostics;
using System.Xml.Serialization;
using Mmi.Communication.Index.Adapter;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model
{
    [XmlRoot]
    [DebuggerDisplay("Type={Type},Name={Name}")]
    public class ListeningModel : IListeningModel
    {
        /// <summary>
        /// 序列化需要一个无参数构造函数
        /// </summary>
        protected ListeningModel()
        {
            
        }

        public ListeningModel(string name = "", CommunicationIndexType type = CommunicationIndexType.InBool)
        {
            Type = type;
            Name = name;
        }

        [XmlAttribute("ListeningType")]
        public CommunicationIndexType Type { get; set; }

        [XmlAttribute("ListeningName")]
        public string Name { get; set; }

    }
}