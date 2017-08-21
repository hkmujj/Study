using System.ComponentModel.Composition;
using General.CIR.Interfaces;
using General.CIR.Models.States;

namespace General.CIR.ViewModels
{
	[Export, Export(typeof(ICIRStatusChanged))]
	public class CalllViewModel : ViewModelBase
	{
		private InputText m_CallNumber;

		public InputText CallNumber
		{
			get
			{
				return m_CallNumber;
			}
			set
			{
				bool flag = Equals(value, m_CallNumber);
				if (!flag)
				{
					m_CallNumber = value;
					RaisePropertyChanged<InputText>(() => CallNumber);
				}
			}
		}

		public override void Initaliation()
		{
			CallNumber = new InputText();
		}

		public void InitText(string value)
		{
			CallNumber.Reset();
			CallNumber.InputString(value);
		}
	}
}
