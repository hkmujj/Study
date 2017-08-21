using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Config
{
	public class OutLib
	{
		public const string File = "OutLib.xml";

		[XmlElement]
		public List<OutLibItem> OutLibItems
		{
			get;
			set;
		}
	}
}
