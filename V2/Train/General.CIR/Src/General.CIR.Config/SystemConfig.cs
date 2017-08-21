using System.Xml.Serialization;

namespace General.CIR.Config
{
	[XmlRoot]
	public class SystemConfig
	{
		public const string File = "SystemConfig.xml";

		[XmlElement]
		public string TrainNumber
		{
			get;
			set;
		}

		[XmlElement]
		public string EngineNumber
		{
			get;
			set;
		}

		[XmlElement]
		public string ColumnEndID
		{
			get;
			set;
		}

		[XmlElement]
		public string Password
		{
			get;
			set;
		}

		[XmlIgnore]
		public string AppConfigPath
		{
			get;
			set;
		}

		public static void Test()
		{
		}
	}
}
