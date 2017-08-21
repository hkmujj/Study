using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using General.CIR.Config;
using General.CIR.Interfaces;
using General.CIR.Models;

namespace General.CIR.ViewModels
{
	[Export, Export(typeof(ICIRStatusChanged)), PartCreationPolicy(CreationPolicy.Shared)]
	public class OutLibViewModel : ViewModelBase
	{
		private ObservableCollection<OutLibItem> m_AllOutLibItems;

		private OutLibItem m_SelectItem;

		public OutLibItem SelectItem
		{
			get
			{
				return m_SelectItem;
			}
			set
			{
				bool flag = Equals(value, m_SelectItem);
				if (!flag)
				{
					m_SelectItem = value;
					RaisePropertyChanged<OutLibItem>(() => SelectItem);
				}
			}
		}

		public ObservableCollection<OutLibItem> AllOutLibItems
		{
			get
			{
				return m_AllOutLibItems;
			}
			set
			{
				bool flag = Equals(value, m_AllOutLibItems);
				if (!flag)
				{
					m_AllOutLibItems = value;
					RaisePropertyChanged<ObservableCollection<OutLibItem>>(() => AllOutLibItems);
				}
			}
		}

		public override void Initaliation()
		{
			AllOutLibItems = new ObservableCollection<OutLibItem>(GlobalParams.Instance.OutLib.OutLibItems);
			SelectItem = AllOutLibItems.FirstOrDefault<OutLibItem>();
		}
	}
}
