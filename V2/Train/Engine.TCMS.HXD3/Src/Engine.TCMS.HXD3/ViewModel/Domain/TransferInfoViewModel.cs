using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export]
    public class TransferInfoViewModel : NotificationObject
    {
        private IEnumerable<TransferInfoUnit> m_DisplayEight;
        private IEnumerable<TransferInfoUnit> m_DisplaySeven;
        private IEnumerable<TransferInfoUnit> m_DisplaySix;
        private IEnumerable<TransferInfoUnit> m_DisplayFive;
        private IEnumerable<TransferInfoUnit> m_DisplayFour;
        private IEnumerable<TransferInfoUnit> m_DisplayThree;
        private IEnumerable<TransferInfoUnit> m_DisplayTwo;
        private IEnumerable<TransferInfoUnit> m_DisplayOne;

        [ImportingConstructor]
        public TransferInfoViewModel(TransferInfoController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            AllCollection = new ObservableCollection<TransferInfoUnit>(GlobalParam.Instance.TransferInfo);
        }

        public TransferInfoController Controller { get; private set; }
        public ObservableCollection<TransferInfoUnit> AllCollection { get; set; }

        public IEnumerable<TransferInfoUnit> DisplayOne
        {
            get { return m_DisplayOne; }
            set
            {
                if (Equals(value, m_DisplayOne))
                {
                    return;
                }
                m_DisplayOne = value;
                RaisePropertyChanged(() => DisplayOne);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplayTwo
        {
            get { return m_DisplayTwo; }
            set
            {
                if (Equals(value, m_DisplayTwo))
                {
                    return;
                }
                m_DisplayTwo = value;
                RaisePropertyChanged(() => DisplayTwo);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplayThree
        {
            get { return m_DisplayThree; }
            set
            {
                if (Equals(value, m_DisplayThree))
                {
                    return;
                }
                m_DisplayThree = value;
                RaisePropertyChanged(() => DisplayThree);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplayFour
        {
            get { return m_DisplayFour; }
            set
            {
                if (Equals(value, m_DisplayFour))
                {
                    return;
                }
                m_DisplayFour = value;
                RaisePropertyChanged(() => DisplayFour);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplayFive
        {
            get { return m_DisplayFive; }
            set
            {
                if (Equals(value, m_DisplayFive))
                {
                    return;
                }
                m_DisplayFive = value;
                RaisePropertyChanged(() => DisplayFive);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplaySix
        {
            get { return m_DisplaySix; }
            set
            {
                if (Equals(value, m_DisplaySix))
                {
                    return;
                }
                m_DisplaySix = value;
                RaisePropertyChanged(() => DisplaySix);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplaySeven
        {
            get { return m_DisplaySeven; }
            set
            {
                if (Equals(value, m_DisplaySeven))
                {
                    return;
                }
                m_DisplaySeven = value;
                RaisePropertyChanged(() => DisplaySeven);
            }
        }

        public IEnumerable<TransferInfoUnit> DisplayEight
        {
            get { return m_DisplayEight; }
            set
            {
                if (Equals(value, m_DisplayEight))
                {
                    return;
                }
                m_DisplayEight = value;
                RaisePropertyChanged(() => DisplayEight);
            }
        }
    }
}