using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using CommonUtil.Util;
using General.CIR.Interfaces;
using General.CIR.Models;
using General.CIR.Models.Units;

namespace General.CIR.ViewModels
{
	[Export, Export(typeof(ICIRStatusChanged))]
	public class PhoneBookViewModel : ViewModelBase
	{
		private Luju m_SelectItems;

		private string m_PageInfo;

		public Luju SelectItems
		{
			get
			{
				return m_SelectItems;
			}
			set
			{
				bool flag = Equals(value, m_SelectItems);
				if (!flag)
				{
					m_SelectItems = value;
					RaisePropertyChanged<Luju>(() => SelectItems);
				}
			}
		}

		public string PageInfo
		{
			get
			{
				return m_PageInfo;
			}
			set
			{
				bool flag = value == m_PageInfo;
				if (!flag)
				{
					m_PageInfo = value;
					RaisePropertyChanged<string>(() => PageInfo);
				}
			}
		}

		public ObservableCollection<Luju> AllLujus
		{
			get;
			private set;
		}

		public override void Initaliation()
		{
			PhoneBook phoneBook = DataSerialization.DeserializeFromXmlFile<PhoneBook>(Path.Combine(GlobalParams.Instance.SystemConfig.AppConfigPath, "Phones.xml"));
			AllLujus = new ObservableCollection<Luju>(phoneBook.AllLuju);
			SelectItems = AllLujus[0];
		}
	}
}
