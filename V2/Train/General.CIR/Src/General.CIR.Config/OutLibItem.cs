using System;
using System.Xml.Serialization;
using General.CIR.Commands;

namespace General.CIR.Config
{
	public class OutLibItem
	{
		private string m_ResponseName;

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

		[XmlIgnore]
		public CustomCommandBase Response
		{
			get;
			set;
		}

		[XmlAttribute]
		public string ResponseName
		{
			get
			{
				return m_ResponseName;
			}
			set
			{
				m_ResponseName = value;
				string typeName = string.Format("General.CIR.Commands.SettingItemResponse.{0}", value);
				Type expr_1B = Type.GetType(typeName);
				Response = (((expr_1B != null) ? expr_1B.Assembly.CreateInstance(typeName) : null) as CustomCommandBase);
			}
		}
	}
}
