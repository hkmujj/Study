using System.ComponentModel.Composition;

namespace General.CIR.ViewModels
{
	[Export]
	public class MainContentViewModel : ViewModelBase
	{
		[Import]
		public SystemInfosViewModel SystemInfosViewModel
		{
			get;
			private set;
		}

		[Import]
		public TitleViewModel TitleViewModel
		{
			get;
			private set;
		}

		[Import]
		public ColumnEndViewModel ColumnEndViewModel
		{
			get;
			private set;
		}

		[Import]
		public ButtonViewModel ButtonViewModel
		{
			get;
			private set;
		}

		[Import]
		public MasterInfoViewModel MasterInfoViewModel
		{
			get;
			private set;
		}

		[Import]
		public PoliceViewModel PoliceViewModel
		{
			get;
			private set;
		}
	}
}
