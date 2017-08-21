using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	[XmlRoot("LineSelect")]
	public class LineSelectAll
	{
		[XmlElement("Bureaus")]
		public List<Bureau> AllLine
		{
			get;
			set;
		}
	}
}
