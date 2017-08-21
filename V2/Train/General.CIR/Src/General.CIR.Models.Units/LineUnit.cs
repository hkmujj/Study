using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	public class LineUnit
	{
		[XmlAttribute]
		public int Index
		{
			get;
			set;
		}

		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}
	}
}
