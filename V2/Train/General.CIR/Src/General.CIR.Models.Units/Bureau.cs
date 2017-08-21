using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	public class Bureau
	{
		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}

		[XmlAttribute]
		public int Index
		{
			get;
			set;
		}

		[XmlElement("LineUnit")]
		public List<LineUnit> AllUnits
		{
			get;
			set;
		}
	}
}
