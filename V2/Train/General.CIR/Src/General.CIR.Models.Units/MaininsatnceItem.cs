using System.Xml.Serialization;
using Microsoft.Practices.Prism.ViewModel;

namespace General.CIR.Models.Units
{
	public class MaininsatnceItem : NotificationObject
	{
		private string m_Content;

		[XmlAttribute]
		public int Index
		{
			get;
			set;
		}

		[XmlAttribute]
		public int Page
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

		[XmlAttribute]
		public string Content
		{
			get
			{
				return m_Content;
			}
			set
			{
				bool flag = value == m_Content;
				if (!flag)
				{
					m_Content = value;
					RaisePropertyChanged<string>(() => Content);
				}
			}
		}
	}
}
