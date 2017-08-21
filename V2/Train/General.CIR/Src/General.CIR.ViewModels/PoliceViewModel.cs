using System.ComponentModel.Composition;
using General.CIR.Controller.ViewModelController;

namespace General.CIR.ViewModels
{
	[Export]
	public class PoliceViewModel : ViewModelBase
	{
		private bool m_IsEmergency;

		private bool m_CanPolice;

		private string m_Trips;

		public PoliceController Controller
		{
			get;
			private set;
		}

		public bool IsEmergency
		{
			get
			{
				return m_IsEmergency;
			}
			set
			{
				bool flag = value == m_IsEmergency;
				if (!flag)
				{
					m_IsEmergency = value;
					RaisePropertyChanged<bool>(() => IsEmergency);
				}
			}
		}

		public bool CanPolice
		{
			get
			{
				return m_CanPolice;
			}
			set
			{
				bool flag = value == m_CanPolice;
				if (!flag)
				{
					m_CanPolice = value;
					RaisePropertyChanged<bool>(() => CanPolice);
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

		[ImportingConstructor]
		public PoliceViewModel(PoliceController controller)
		{
			controller.ViewModel = this;
			Controller = controller;
		}
	}
}
