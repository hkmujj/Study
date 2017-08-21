using System.ComponentModel.Composition;
using General.CIR.Controller.ViewModelController;

namespace General.CIR.ViewModels
{
	[Export]
	public class SystemInfosViewModel : ViewModelBase
	{
		private bool m_LineIsAuto;

		private string m_LineName;

		private double m_KMMark;

		private bool m_IsSupplyOrder;

		private bool m_IsShuntTrain;

		private string m_SignalModel;

		public SystemInfoContrller Contrller
		{
			get;
			private set;
		}

		public bool LineIsAuto
		{
			get
			{
				return m_LineIsAuto;
			}
			set
			{
				bool flag = value == m_LineIsAuto;
				if (!flag)
				{
					m_LineIsAuto = value;
					RaisePropertyChanged<bool>(() => LineIsAuto);
				}
			}
		}

		public string LineName
		{
			get
			{
				return m_LineName;
			}
			set
			{
				bool flag = value == m_LineName;
				if (!flag)
				{
					m_LineName = value;
					RaisePropertyChanged<string>(() => LineName);
				}
			}
		}

		public double KMMark
		{
			get
			{
				return m_KMMark;
			}
			set
			{
				bool flag = value.Equals(m_KMMark);
				if (!flag)
				{
					m_KMMark = value;
					RaisePropertyChanged<double>(() => KMMark);
				}
			}
		}

		public bool IsSupplyOrder
		{
			get
			{
				return m_IsSupplyOrder;
			}
			set
			{
				bool flag = value == m_IsSupplyOrder;
				if (!flag)
				{
					m_IsSupplyOrder = value;
					RaisePropertyChanged<bool>(() => IsSupplyOrder);
				}
			}
		}

		public bool IsShuntTrain
		{
			get
			{
				return m_IsShuntTrain;
			}
			set
			{
				bool flag = value == m_IsShuntTrain;
				if (!flag)
				{
					m_IsShuntTrain = value;
					RaisePropertyChanged<bool>(() => IsShuntTrain);
				}
			}
		}

		public string SignalModel
		{
			get
			{
				return m_SignalModel;
			}
			set
			{
				bool flag = value == m_SignalModel;
				if (!flag)
				{
					m_SignalModel = value;
					RaisePropertyChanged<string>(() => SignalModel);
				}
			}
		}

		[ImportingConstructor]
		public SystemInfosViewModel(SystemInfoContrller contrller)
		{
			contrller.ViewModel = this;
			Contrller = contrller;
		}
	}
}
