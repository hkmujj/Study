using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export(typeof(IResetSupport))]
    [Export]
    public class ZeroLevelTestViewModel : TestViewModelBase
    {
        private TestResult m_TestResult6;
        private TestResult m_TestResult5;
        private TestResult m_TestResult3;
        private TestResult m_TestResult4;
        private TestResult m_TestResult2;
        private TestResult m_TestResult1;
        private bool m_ResultVisible;

        [ImportingConstructor]
        public ZeroLevelTestViewModel(TestController controller)
        {
            StateKeyName = StateKeys.Root_检修状态_试验_零级位;
            Controller = controller;
            controller.ViewModel = this;
        }

        public bool ResultVisible
        {
            set
            {
                if (value == m_ResultVisible)
                {
                    return;
                }

                m_ResultVisible = value;
                RaisePropertyChanged(() => ResultVisible);
            }
            get { return m_ResultVisible; }
        }

        public TestResult TestResult1
        {
            set
            {
                if (value == m_TestResult1)
                {
                    return;
                }

                m_TestResult1 = value;
                RaisePropertyChanged(() => TestResult1);
            }
            get { return m_TestResult1; }
        }

        public TestResult TestResult2
        {
            set
            {
                if (value == m_TestResult2)
                {
                    return;
                }

                m_TestResult2 = value;
                RaisePropertyChanged(() => TestResult2);
            }
            get { return m_TestResult2; }
        }

        public TestResult TestResult3
        {
            set
            {
                if (value == m_TestResult3)
                {
                    return;
                }

                m_TestResult3 = value;
                RaisePropertyChanged(() => TestResult3);
            }
            get { return m_TestResult3; }
        }

        public TestResult TestResult4
        {
            set
            {
                if (value == m_TestResult4)
                {
                    return;
                }

                m_TestResult4 = value;
                RaisePropertyChanged(() => TestResult4);
            }
            get { return m_TestResult4; }
        }

        public TestResult TestResult5
        {
            set
            {
                if (value == m_TestResult5)
                {
                    return;
                }

                m_TestResult5 = value;
                RaisePropertyChanged(() => TestResult5);
            }
            get { return m_TestResult5; }
        }

        public TestResult TestResult6
        {
            set
            {
                if (value == m_TestResult6)
                {
                    return;
                }

                m_TestResult6 = value;
                RaisePropertyChanged(() => TestResult6);
            }
            get { return m_TestResult6; }
        }

        public override void RestartTest()
        {
            ResultVisible = false;
            TestResult1 = TestResult.Unkown;
            TestResult2 = TestResult.Unkown;
            TestResult3 = TestResult.Unkown;
            TestResult4 = TestResult.Unkown;
            TestResult5 = TestResult.Unkown;
            TestResult6 = TestResult.Unkown;
        }
    }
}