using System;
using System.ComponentModel.Composition;
using General.CIR.Controller.ViewModelController;
using General.CIR.Interfaces;

namespace General.CIR.ViewModels
{
	[Export, Export(typeof(ICIRStatusChanged))]
	public class TitleViewModel : ViewModelBase
	{
		private bool m_IsSpeaker;

		private double m_SpeakerSound;

		private double m_Signal;

		private double m_ReciverSound;

		private bool m_EngineNumberIsRegister;

		private string m_EngineNumber;

		private string m_TrainNumber;

		private bool m_TrainNumberIsRegister;

		private bool m_TrainNumberIsAuto;

	
		public event Action<bool> TrainNumberIsRegisterChanged;

		public TitleController Controller
		{
			get;
			private set;
		}

		public bool TrainNumberIsAuto
		{
			get
			{
				return m_TrainNumberIsAuto;
			}
			set
			{
				bool flag = value == m_TrainNumberIsAuto;
				if (!flag)
				{
					m_TrainNumberIsAuto = value;
					RaisePropertyChanged<bool>(() => TrainNumberIsAuto);
				}
			}
		}

		public bool TrainNumberIsRegister
		{
			get
			{
				return m_TrainNumberIsRegister;
			}
			set
			{
				OnTrainNumberIsRegisterChanged(value);
				bool flag = value == m_TrainNumberIsRegister;
				if (!flag)
				{
					m_TrainNumberIsRegister = value;
					RaisePropertyChanged<bool>(() => TrainNumberIsRegister);
				}
			}
		}

		public string TrainNumber
		{
			get
			{
				return m_TrainNumber;
			}
			set
			{
				bool flag = value == m_TrainNumber;
				if (!flag)
				{
					m_TrainNumber = value;
					RaisePropertyChanged<string>(() => TrainNumber);
				}
			}
		}

		public string EngineNumber
		{
			get
			{
				return m_EngineNumber;
			}
			set
			{
				bool flag = value == m_EngineNumber;
				if (!flag)
				{
					m_EngineNumber = value;
					RaisePropertyChanged<string>(() => EngineNumber);
				}
			}
		}

		public bool EngineNumberIsRegister
		{
			get
			{
				return m_EngineNumberIsRegister;
			}
			set
			{
				bool flag = value == m_EngineNumberIsRegister;
				if (!flag)
				{
					m_EngineNumberIsRegister = value;
					RaisePropertyChanged<bool>(() => EngineNumberIsRegister);
				}
			}
		}

		public double ReciverSound
		{
			get
			{
				return m_ReciverSound;
			}
			set
			{
				bool flag = value.Equals(m_ReciverSound) || value < 0.0;
				if (!flag)
				{
					m_ReciverSound = value;
					RaisePropertyChanged<double>(() => ReciverSound);
				}
			}
		}

		public double Signal
		{
			get
			{
				return m_Signal;
			}
			set
			{
				bool flag = value.Equals(m_Signal);
				if (!flag)
				{
					m_Signal = value;
					RaisePropertyChanged<double>(() => Signal);
				}
			}
		}

		public double SpeakerSound
		{
			get
			{
				return m_SpeakerSound;
			}
			set
			{
				bool flag = value.Equals(m_SpeakerSound) || value < 0.0;
				if (!flag)
				{
					m_SpeakerSound = value;
					RaisePropertyChanged<double>(() => SpeakerSound);
				}
			}
		}

		public bool IsSpeaker
		{
			get
			{
				return m_IsSpeaker;
			}
			set
			{
				bool flag = value == m_IsSpeaker;
				if (!flag)
				{
					m_IsSpeaker = value;
					RaisePropertyChanged<bool>(() => IsSpeaker);
				}
			}
		}

		[ImportingConstructor]
		public TitleViewModel(TitleController controller)
		{
			controller.ViewModel = this;
			Controller = controller;
		}

		public override void Initaliation()
		{
			SpeakerSound = 8.0;
			ReciverSound = 8.0;
			Signal = 4.0;
			TrainNumberIsAuto = true;
		}

		protected virtual void OnTrainNumberIsRegisterChanged(bool obj)
		{
			Action<bool> expr_07 = TrainNumberIsRegisterChanged;
			if (expr_07 != null)
			{
				expr_07(obj);
			}
		}
	}
}
