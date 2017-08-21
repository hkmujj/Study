using System.Collections.Generic;
using LightRail.HMI.SZLHLF.Controller;
using System.ComponentModel.Composition;

namespace LightRail.HMI.SZLHLF.ViewModel
{
    [Export(typeof(InfoSetModel))]
    public class InfoSetModel : ViewModelBase
    {
        [ImportingConstructor]
        public InfoSetModel(InfoSetController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
        }

        public InfoSetController Controller { get; private set; }

        public bool CurIsCh
        {
            get { return m_CurIsCh; }
            set
            {
                if (value == m_CurIsCh)
                {
                    return;
                }
                m_CurIsCh = value;
                RaisePropertyChanged(() => CurIsCh);
            }
        }

        private bool m_CurIsCh;

        public bool NewIsCh
        {
            get { return m_NewIsCh; }
            set
            {
                if (value == m_NewIsCh)
                {
                    return;
                }
                m_NewIsCh = value;
                RaisePropertyChanged(() => NewIsCh);
            }
        }
        private bool m_NewIsCh;

        public OtherViewModel ViewModel { get; set; }

        public Dictionary<string, string> InitDicToDictionary;

   
    }
}
