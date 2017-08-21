using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	[XmlRoot]
	public class PhoneBook
	{
		[XmlElement("LuJu")]
		public List<Luju> AllLuju
		{
			get;
			set;
		}
	}
}
