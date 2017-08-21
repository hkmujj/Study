using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	public class Luju
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

		[XmlElement("Lines")]
		public List<LinePhone> LinePhones
		{
			get;
			set;
		}
	}
}
