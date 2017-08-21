using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	public class PhonesUnit
	{
		[XmlAttribute]
		public string Name
		{
			get;
			set;
		}

		[XmlAttribute]
		public string PhoneNumber
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
	}
}
