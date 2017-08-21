using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using CommonUtil.Util;
using General.CIR.Controller.ViewModelController;
using General.CIR.Interfaces;
using General.CIR.Models;
using General.CIR.Models.Units;

namespace General.CIR.ViewModels
{
	[Export, Export(typeof(ICIRStatusChanged))]
	public class LineSelectViewModel : ViewModelBase
	{
		private Bureau m_SelectItems;

		public LineSelectController Controller
		{
			get; }

		public string PageInfo
		{
			get;
			set;
		}

		public ObservableCollection<Bureau> Bureaus
		{
			get;
			private set;
		}

		public Bureau SelectItems
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
					RaisePropertyChanged<Bureau>(() => SelectItems);
				}
			}
		}

		[ImportingConstructor]
		public LineSelectViewModel(LineSelectController controller)
		{
			Controller = controller;
			Controller.ViewModel = this;
			Controller.Initialize();
		}

		public override void Initaliation()
		{
			LineSelectAll lineSelectAll = DataSerialization.DeserializeFromXmlFile<LineSelectAll>(Path.Combine(GlobalParams.Instance.SystemConfig.AppConfigPath, "Line.xml"));
			Bureaus = new ObservableCollection<Bureau>(lineSelectAll.AllLine);
			SelectItems = Bureaus[0];
		}
	}
}
