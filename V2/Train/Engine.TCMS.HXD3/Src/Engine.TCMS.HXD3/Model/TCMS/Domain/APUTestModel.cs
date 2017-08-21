using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Microsoft.Practices.Prism.ViewModel;

namespace Engine.TCMS.HXD3.Model.TCMS.Domain
{
    public class APUTestModel : NotificationObject
    {
        private bool m_Visible;
        private TestResult m_TestResult;

        public APUTestModel()
        {
            APU = new APU();
        }

        public APU APU { private set; get; }

        public bool Visible
        {
            set
            {
                if (value == m_Visible)
                {
                    return;
                }

                m_Visible = value;
                RaisePropertyChanged(() => Visible);
            }
            get { return m_Visible; }
        }

        public TestResult TestResult
        {
            set
            {
                if (value == m_TestResult)
                {
                    return;
                }

                m_TestResult = value;
                RaisePropertyChanged(() => TestResult);
            }
            get { return m_TestResult; }
        }
    }
}