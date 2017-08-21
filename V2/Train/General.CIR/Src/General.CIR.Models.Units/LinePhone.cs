using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	public class LinePhone
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

		[XmlElement]
		public List<PhonesUnit> AllPhone
		{
			get;
			set;
		}
	}
}
