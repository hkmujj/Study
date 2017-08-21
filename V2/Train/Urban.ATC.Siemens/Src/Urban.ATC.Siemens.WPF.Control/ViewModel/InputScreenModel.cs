using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using Urban.ATC.Siemens.Model;
using Urban.ATC.Siemens.Resource.Internal;
using Urban.ATC.Siemens.WPF.Interface;

namespace Urban.ATC.Siemens.WPF.Control.ViewModel
{
    public class InputScreenModel : ViewModelBase
    {
        private string m_DestinationNum;
        private string m_TripNum;
        private string m_CrewNum;
        private string m_InputType;
        private string m_DisplayNumber;

        public InputScreenModel(SiemensData data)
        {
            Parent = data;
            Submit = new DelegateCommand<string>(SubmitModth);
            Input = new DelegateCommand<string>(InputModth);
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
            if (InputType.Equals(InputTypeName.Crew))
            {
                CrewNum = sPara;
                var index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.工号];

                Parent.RegionT.CrewNumber = Parent.DataService.ReadService.GetFloatAt(index) > float.Epsilon ? Parent.RegionT.CrewNumber : tmp;
            }
            else if (InputType.Equals(InputTypeName.Trip))
            {
                TripNum = sPara;
                var index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.服务号];
                Parent.RegionT.TripNumber = Parent.DataService.ReadService.GetFloatAt(index) > double.Epsilon ? Parent.RegionT.TripNumber : "T" + tmp.ToString("000000");
            }
            else if (InputType.Equals(InputTypeName.Destination))
            {
                DestinationNum = sPara;
                var index = IndexConfigure.Instance.IndexFacade.InFloatDictionary[InFloatKeys.目的地号];
                Parent.RegionT.DestinationNumber = Parent.DataService.ReadService.GetFloatAt(index) > double.Epsilon ? Parent.RegionT.DestinationNumber : tmp;
            }
        }

        public ICommand Input { get; private set; }
        public ICommand Submit { get; private set; }

        public string InputType
        {
            get { return m_InputType; }
            set
            {
                if (value == m_InputType)
                {
                    return;
                }
                m_InputType = value;
                RaisePropertyChanged(() => InputType);
            }
        }

        public string CrewNum
        {
            get { return m_CrewNum; }
            set
            {
                if (value == m_CrewNum)
                {
                    return;
                }
                m_CrewNum = value;
                RaisePropertyChanged(() => CrewNum);
            }
        }

        public string TripNum
        {
            get { return m_TripNum; }
            set
            {
                if (value == m_TripNum)
                {
                    return;
                }
                m_TripNum = value;
                RaisePropertyChanged(() => TripNum);
            }
        }

        public string DestinationNum
        {
            get { return m_DestinationNum; }
            set
            {
                if (value == m_DestinationNum)
                {
                    return;
                }
                m_DestinationNum = value;
                RaisePropertyChanged(() => DestinationNum);
            }
        }
    }
}