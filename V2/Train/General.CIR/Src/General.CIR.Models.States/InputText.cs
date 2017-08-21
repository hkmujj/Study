using System.ComponentModel;
using General.CIR.Extentions;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.View;

namespace General.CIR.Models.States
{
	public class InputText : NotificationObject, ICaretTextModel, INotifyPropertyChanged
	{
		private int m_BindableCaretIndex;

		private string m_Text;

		public int BindableCaretIndex
		{
			get
			{
				return m_BindableCaretIndex;
			}
			set
			{
				bool flag = value == m_BindableCaretIndex;
				if (!flag)
				{
					m_BindableCaretIndex = value;
					RaisePropertyChanged<int>(() => BindableCaretIndex);
				}
			}
		}

		public string Text
		{
			get
			{
				return m_Text;
			}
			set
			{
				bool flag = value == m_Text;
				if (!flag)
				{
					m_Text = value;
					RaisePropertyChanged<string>(() => Text);
				}
			}
		}

		public InputText()
		{
			Reset();
		}

		public void Reset()
		{
			BindableCaretIndex = 0;
			Text = string.Empty;
		}

		public void InputString(string value)
		{
			Text = Text.Insert(BindableCaretIndex, value);
			int bindableCaretIndex = BindableCaretIndex;
			BindableCaretIndex = bindableCaretIndex + 1;
		}

		public void Delete()
		{
			string text = Text;
			Text = Text.RemoveAt(BindableCaretIndex - 1);
			bool flag = text != Text;
			if (flag)
			{
				int bindableCaretIndex = BindableCaretIndex;
				BindableCaretIndex = bindableCaretIndex - 1;
			}
		}
	}
}
