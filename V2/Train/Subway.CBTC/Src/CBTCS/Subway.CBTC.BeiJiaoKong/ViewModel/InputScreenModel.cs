using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;


namespace Subway.CBTC.BeiJiaoKong.ViewModel
{
    public class InputScreenModel : ViewModelBase
    {
        private string m_DriverNum;
        private string m_DisplayNumber;

        public InputScreenModel(BeiJiaoKongViewModel model)
        {
            Parent = model;
            Input = new DelegateCommand<string>(InputModth);
            Submit = new DelegateCommand<string>(SubmitModth);
        }


        public string DisplayNumber
        {
            get { return m_DisplayNumber; }
            set
            {
                if (value == m_DisplayNumber)
                {
                    return;
                }
                m_DisplayNumber = value;
                RaisePropertyChanged(() => DisplayNumber);
            }
        }

        private void InputModth(string obj)
        {
            DisplayNumber = obj;
        }

        private void SubmitModth(string sPara)
        {
            double tmp;
            double.TryParse(sPara, out tmp);
            DriverNum = sPara;
            
        }

        public ICommand Input { get; private set; }
        public ICommand Submit { get; private set; }

        public string DriverNum
        {
            get { return m_DriverNum; }
            set
            {
                if (value == m_DriverNum)
                {
                    return;
                }
                m_DriverNum = value;
                RaisePropertyChanged(() => DriverNum);
            }
        }
    }
}
