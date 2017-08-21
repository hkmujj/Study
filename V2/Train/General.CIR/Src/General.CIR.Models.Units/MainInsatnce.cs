using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Models.Units
{
	public class MainInsatnce
	{
		public const string File = "MainInstacne.xml";

		[XmlElement("Item")]
		public List<MaininsatnceItem> AllItems
		{
			get;
			set;
		}
	}
}
