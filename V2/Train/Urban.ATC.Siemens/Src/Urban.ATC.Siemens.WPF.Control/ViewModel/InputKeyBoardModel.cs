using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class InputKeyBoardModel : ViewModelBase
    {
        private string m_InputNumbers;

        public InputKeyBoardModel(SiemensData data)
        {
            Parent = data;
            InputConmand = new DelegateCommand<string>(InputNumber);
            SubmitCommand = new DelegateCommand(Submit);
            InputNumbers = string.Empty;
        }

        private void Submit()
        {
            Parent.InputScreen.Submit.Execute(InputNumbers);
            //Parent.InputView.Sunbmit(InputNumbers);
        }

        public string InputNumbers
        {
            get { return m_InputNumbers; }
            set
            {
                if (value == m_InputNumbers)
                {
                    return;
                }
                m_InputNumbers = value;
                RaisePropertyChanged(() => InputNumbers);
            }
        }

        private void InputNumber(string obj)
        {
            if (obj.Contains("C") && InputNumbers.Length > 1)
            {
                InputNumbers = InputNumbers.Substring(0, InputNumbers.Length - 1);
            }
            else
            {
                InputNumbers += obj;
            }
            Parent.InputScreen.Input.Execute(InputNumbers);
            //Parent.InputView.Input(InputNumbers);
        }

        public ICommand SubmitCommand { get; private set; }
        public ICommand InputConmand { get; private set; }
    }
}