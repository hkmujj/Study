using System.ComponentModel.Composition;
using General.CIR.Controller.ViewModelController;

namespace General.CIR.ViewModels
{
	[Export]
	public class MasterInfoViewModel : ViewModelBase
	{
		private string m_CallName;

		private string m_CenterInfo;

		public MasterInfoController Controller
		{
			get; }

		public string CallName
		{
			get
			{
				return m_CallName;
			}
			set
			{
				bool flag = value == m_CallName;
				if (!flag)
				{
					m_CallName = value;
					RaisePropertyChanged<string>(() => CallName);
				}
			}
		}

		public string CenterInfo
		{
			get
			{
				return m_CenterInfo;
			}
			set
			{
				bool flag = value == m_CenterInfo;
				if (!flag)
				{
					m_CenterInfo = value;
					RaisePropertyChanged<string>(() => CenterInfo);
				}
			}
		}

		[ImportingConstructor]
		public MasterInfoViewModel(MasterInfoController controller)
		{
			Controller = controller;
			Controller.ViewModel = this;
			Controller.Initialize();
		}
	}
}
