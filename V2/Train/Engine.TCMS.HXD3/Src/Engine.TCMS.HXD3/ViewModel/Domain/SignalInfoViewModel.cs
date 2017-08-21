using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model;
using Engine.TCMS.HXD3.Model.ConfigModel;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export(typeof(SignalInfoViewModel))]
    public class SignalInfoViewModel : NotificationObject
    {
        private IEnumerable<SignalInfoUnit> m_DisPlaySignalInfo;
        private int m_Page;

        [ImportingConstructor]
        public SignalInfoViewModel(SignalInfoController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            AllSignalInfo = new ObservableCollection<SignalInfoUnit>(GlobalParam.Instance.SignalInfo);
        }

        public int Page
        {
            get { return m_Page; }
            set
            {
                if (value == m_Page)
                {
                    return;
                }

                m_Page = value;
                RaisePropertyChanged(() => Page);
            }
        }

        public ObservableCollection<SignalInfoUnit> AllSignalInfo { get; private set; }

        public SignalInfoController Controller { get; private set; }

        public IEnumerable<SignalInfoUnit> DisPlaySignalInfo
        {
            get { return m_DisPlaySignalInfo; }
            set
            {
                if (Equals(value, m_DisPlaySignalInfo))
                {
                    return;
                }

                m_DisPlaySignalInfo = value;
                RaisePropertyChanged(() => DisPlaySignalInfo);
            }
        }
    }
}