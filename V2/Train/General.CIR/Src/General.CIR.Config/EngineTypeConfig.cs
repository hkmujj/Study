using System.Collections.Generic;
using System.Xml.Serialization;

namespace General.CIR.Config
{
	public class EngineTypeConfig
	{
		[XmlElement]
		public List<EngineType> EngineTypes
		{
			get;
			set;
		}
	}
}
