using System.ComponentModel.Composition;
using Engine._6A.Interface.ViewModel.DataMonitor;
using Engine._6A.ViewModel.Common;

namespace Engine._6A.ViewModel.Axle6
{
    [Export(typeof(IRunLineTwoViewModel))]
    public class RunLineTwoViewModel : ViewModelBase, IRunLineTwoViewModel
    {
        private string m_TwoEndBody;
        private string m_OneEndBody;
        private string m_TwoEndCarcass;
        private string m_OneEndCarcass;
        private string m_ReferenceVelocity;

        public string ReferenceVelocity
        {
            get { return m_ReferenceVelocity; }
            set
            {
                if (value == m_ReferenceVelocity)
                {
                    return;
                }
                m_ReferenceVelocity = value;
                RaisePropertyChanged(() => ReferenceVelocity);
            }
        }

        public string OneEndCarcass
        {
            get { return m_OneEndCarcass; }
            set
            {
                if (value == m_OneEndCarcass)
                {
                    return;
                }
                m_OneEndCarcass = value;
                RaisePropertyChanged(() => OneEndCarcass);
            }
        }

        public string TwoEndCarcass
        {
            get { return m_TwoEndCarcass; }
            set
            {
                if (value == m_TwoEndCarcass)
                {
                    return;
                }
                m_TwoEndCarcass = value;
                RaisePropertyChanged(() => TwoEndCarcass);
            }
        }

        public string OneEndBody
        {
            get { return m_OneEndBody; }
            set
            {
                if (value == m_OneEndBody)
                {
                    return;
                }
                m_OneEndBody = value;
                RaisePropertyChanged(() => OneEndBody);
            }
        }

        public string TwoEndBody
        {
            get { return m_TwoEndBody; }
            set
            {
                if (value == m_TwoEndBody)
                {
                    return;
                }
                m_TwoEndBody = value;
                RaisePropertyChanged(() => TwoEndBody);
            }
        }
    }
}