using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Subway.CBTC.BeiJiaoKong.Events;
using Subway.CBTC.BeiJiaoKong.Models.Domain;

namespace Subway.CBTC.BeiJiaoKong.ViewModel
{
    public class InputKeyBoardModel : ViewModelBase
    {
        private string m_InputNumbers;

        public InputKeyBoardModel(BeiJiaoKongViewModel model)
        {
            Parent = model;
            InputConmand = new DelegateCommand<string>(InputNumber);
            SubmitCommand = new DelegateCommand<NavigatorEventArgs>(Submit);
            ClearCommand = new DelegateCommand(() =>
            {
                InputNumbers = string.Empty;
            });
            InputNumbers = string.Empty;
        }

        private void Submit(NavigatorEventArgs eventArgs)
        {
            if (InputNumbers.Length == (int)DriverNumber.TCTDriverNumber)
            {
                Parent.InputScreen.Submit.Execute(InputNumbers);
                Parent.Controller.Navigator.Execute(eventArgs);
            }

            InputNumbers = string.Empty;
            Parent.InputScreen.Input.Execute(InputNumbers);
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
            if (obj.Contains("C") && InputNumbers.Length > 0)
            {
                InputNumbers = InputNumbers.Substring(0, InputNumbers.Length - 1);
            }
            else if (!obj.Contains("C") && InputNumbers.Length < (int) DriverNumber.TCTDriverNumber)
            {
                InputNumbers += obj;
            }
            Parent.InputScreen.Input.Execute(InputNumbers);
        }

        public ICommand SubmitCommand { get; private set; }
        public ICommand InputConmand { get; private set; }
        public ICommand ClearCommand { get; private set; }
    }
}
