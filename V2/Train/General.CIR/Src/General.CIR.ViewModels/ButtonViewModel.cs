using System.ComponentModel.Composition;
using General.CIR.Controller.ViewModelController;

namespace General.CIR.ViewModels
{
	[Export]
	public class ButtonViewModel : ViewModelBase
	{
		private string m_F1;

		private string m_F2;

		private string m_F3;

		private string m_F4;

		private string m_F5;

		private string m_F6;

		private string m_F7;

		private string m_F8;

		private bool m_IsCalling;

		public ButtonController Controller
		{
			get; }

		public bool IsCalling
		{
			get
			{
				return m_IsCalling;
			}
			set
			{
				bool flag = value == m_IsCalling;
				if (!flag)
				{
					m_IsCalling = value;
					Controller.UpdateStates();
					RaisePropertyChanged<bool>(() => IsCalling);
				}
			}
		}

		public string F1
		{
			get
			{
				return m_F1;
			}
			set
			{
				bool flag = value == m_F1;
				if (!flag)
				{
					m_F1 = value;
					RaisePropertyChanged<string>(() => F1);
				}
			}
		}

		public string F2
		{
			get
			{
				return m_F2;
			}
			set
			{
				bool flag = value == m_F2;
				if (!flag)
				{
					m_F2 = value;
					RaisePropertyChanged<string>(() => F2);
				}
			}
		}

		public string F3
		{
			get
			{
				return m_F3;
			}
			set
			{
				bool flag = value == m_F3;
				if (!flag)
				{
					m_F3 = value;
					RaisePropertyChanged<string>(() => F3);
				}
			}
		}

		public string F4
		{
			get
			{
				return m_F4;
			}
			set
			{
				bool flag = value == m_F4;
				if (!flag)
				{
					m_F4 = value;
					RaisePropertyChanged<string>(() => F4);
				}
			}
		}

		public string F5
		{
			get
			{
				return m_F5;
			}
			set
			{
				bool flag = value == m_F5;
				if (!flag)
				{
					m_F5 = value;
					RaisePropertyChanged<string>(() => F5);
				}
			}
		}

		public string F6
		{
			get
			{
				return m_F6;
			}
			set
			{
				bool flag = value == m_F6;
				if (!flag)
				{
					m_F6 = value;
					RaisePropertyChanged<string>(() => F6);
				}
			}
		}

		public string F7
		{
			get
			{
				return m_F7;
			}
			set
			{
				bool flag = value == m_F7;
				if (!flag)
				{
					m_F7 = value;
					RaisePropertyChanged<string>(() => F7);
				}
			}
		}

		public string F8
		{
			get
			{
				return m_F8;
			}
			set
			{
				bool flag = value == m_F8;
				if (!flag)
				{
					m_F8 = value;
					RaisePropertyChanged<string>(() => F8);
				}
			}
		}

		[ImportingConstructor]
		public ButtonViewModel(ButtonController controller)
		{
			Controller = controller;
			Controller.ViewModel = this;
			Controller.Initialize();
		}
	}
}
