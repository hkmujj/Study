using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Excel.Interface;
using General.CIR.Controller.ViewModelController;
using General.CIR.Interfaces;
using General.CIR.Models;
using General.CIR.Models.Units;

namespace General.CIR.ViewModels
{
	[Export, Export(typeof(ICIRStatusChanged))]
	public class DispatchCommandViewModel : ViewModelBase
	{
		private ObservableCollection<DispatchCommandUnit> m_DisplayUnits;

		private ObservableCollection<DispatchCommandUnit> m_AllUnit;

		private DispatchCommandUnit m_SelectUnit;

		private DispatchCommandUnit m_DisplayUnit;

		private string m_Trips;

		private string m_CommandTitle;

		private ObservableCollection<SerchItem> m_AllSerchItems;

		private SerchItem m_SelectSerchItem;

		public DispatchCommandController Controller
		{
			get;
			private set;
		}

		public ObservableCollection<DispatchCommandUnit> AllUnit
		{
			get
			{
				return m_AllUnit;
			}
			private set
			{
				bool flag = Equals(value, m_AllUnit);
				if (!flag)
				{
					m_AllUnit = value;
					RaisePropertyChanged<ObservableCollection<DispatchCommandUnit>>(() => AllUnit);
				}
			}
		}

		public ObservableCollection<DispatchCommandUnit> DisplayUnits
		{
			get
			{
				return m_DisplayUnits;
			}
			set
			{
				bool flag = Equals(value, m_DisplayUnits);
				if (!flag)
				{
					m_DisplayUnits = value;
					RaisePropertyChanged<ObservableCollection<DispatchCommandUnit>>(() => DisplayUnits);
				}
			}
		}

		public DispatchCommandUnit SelectUnit
		{
			get
			{
				return m_SelectUnit;
			}
			set
			{
				bool flag = Equals(value, m_SelectUnit);
				if (!flag)
				{
					m_SelectUnit = value;
					RaisePropertyChanged<DispatchCommandUnit>(() => SelectUnit);
				}
			}
		}

		public DispatchCommandUnit DisplayUnit
		{
			get
			{
				return m_DisplayUnit;
			}
			set
			{
				bool flag = Equals(value, m_DisplayUnit);
				if (!flag)
				{
					m_DisplayUnit = value;
					RaisePropertyChanged<DispatchCommandUnit>(() => DisplayUnit);
				}
			}
		}

		public string Trips
		{
			get
			{
				return m_Trips;
			}
			set
			{
				bool flag = value == m_Trips;
				if (!flag)
				{
					m_Trips = value;
					RaisePropertyChanged<string>(() => Trips);
				}
			}
		}

		public string CommandTitle
		{
			get
			{
				return m_CommandTitle;
			}
			set
			{
				bool flag = value == m_CommandTitle;
				if (!flag)
				{
					m_CommandTitle = value;
					RaisePropertyChanged<string>(() => CommandTitle);
				}
			}
		}

		public ObservableCollection<SerchItem> AllSerchItems
		{
			get
			{
				return m_AllSerchItems;
			}
			set
			{
				bool flag = Equals(value, m_AllSerchItems);
				if (!flag)
				{
					m_AllSerchItems = value;
					RaisePropertyChanged<ObservableCollection<SerchItem>>(() => AllSerchItems);
				}
			}
		}

		public SerchItem SelectSerchItem
		{
			get
			{
				return m_SelectSerchItem;
			}
			set
			{
				bool flag = Equals(value, m_SelectSerchItem);
				if (!flag)
				{
					m_SelectSerchItem = value;
					CommandTitle = value.Parmers;
					RaisePropertyChanged<SerchItem>(() => SelectSerchItem);
				}
			}
		}

		[ImportingConstructor]
		public DispatchCommandViewModel(DispatchCommandController controller)
		{
			Controller = controller;
			controller.ViewModel = this;
			AllUnit = new ObservableCollection<DispatchCommandUnit>();
			AllSerchItems = new ObservableCollection<SerchItem>();
		}

		public override void Initaliation()
		{
			AllUnit.Clear();
			IEnumerable<SerchItem> collection = ExcelParser.Parser<SerchItem>(GlobalParams.Instance.SystemConfig.AppConfigPath);
			AllSerchItems = new ObservableCollection<SerchItem>(collection);
		}
	}
}
