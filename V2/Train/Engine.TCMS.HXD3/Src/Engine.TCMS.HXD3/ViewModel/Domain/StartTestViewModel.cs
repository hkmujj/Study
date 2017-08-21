using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export(typeof(IResetSupport))]
    [Export]
    public class StartTestViewModel : TestViewModelBase
    {
        private TestResult m_ResultCI6;
        private TestResult m_ResultCI5;
        private TestResult m_ResultCI4;
        private TestResult m_ResultCI3;
        private TestResult m_ResultCI2;
        private TestResult m_ResultCI1;
        private string m_CuurentCI6;
        private string m_CuurentCI5;
        private string m_CuurentCI4;
        private string m_CuurentCI3;
        private string m_CuurentCI2;
        private string m_CuurentCI1;

        [ImportingConstructor]
        public StartTestViewModel(TestController controller)
        {
            Controller = controller;
            Controller.ViewModel = this;
            StateKeyName = StateKeys.Root_检修状态_试验_起动;
        }

        /// <summary>
        /// 牵引电机电流CI1
        /// </summary>
        public string CuurentCI1
        {
            get { return m_CuurentCI1; }
            set
            {
                if (value.Equals(m_CuurentCI1))
                {
                    return;
                }
                m_CuurentCI1 = value;
                RaisePropertyChanged(() => CuurentCI1);
            }
        }
        /// <summary>
        /// 牵引电机电流CI2
        /// </summary>
        public string CuurentCI2
        {
            get { return m_CuurentCI2; }
            set
            {
                if (value.Equals(m_CuurentCI2))
                {
                    return;
                }
                m_CuurentCI2 = value;
                RaisePropertyChanged(() => CuurentCI2);
            }
        }
        /// <summary>
        /// 牵引电机电流CI3
        /// </summary>
        public string CuurentCI3
        {
            get { return m_CuurentCI3; }
            set
            {
                if (value.Equals(m_CuurentCI3))
                {
                    return;
                }
                m_CuurentCI3 = value;
                RaisePropertyChanged(() => CuurentCI3);
            }
        }
        /// <summary>
        /// 牵引电机电流CI4
        /// </summary>
        public string CuurentCI4
        {
            get { return m_CuurentCI4; }
            set
            {
                if (value.Equals(m_CuurentCI4))
                {
                    return;
                }
                m_CuurentCI4 = value;
                RaisePropertyChanged(() => CuurentCI4);
            }
        }
        /// <summary>
        /// 牵引电机电流CI5
        /// </summary>
        public string CuurentCI5
        {
            get { return m_CuurentCI5; }
            set
            {
                if (value.Equals(m_CuurentCI5))
                {
                    return;
                }
                m_CuurentCI5 = value;
                RaisePropertyChanged(() => CuurentCI5);
            }
        }
        /// <summary>
        /// 牵引电机电流CI6
        /// </summary>
        public string CuurentCI6
        {
            get { return m_CuurentCI6; }
            set
            {
                if (value.Equals(m_CuurentCI6))
                {
                    return;
                }
                m_CuurentCI6 = value;
                RaisePropertyChanged(() => CuurentCI6);
            }
        }
        /// <summary>
        /// 测试结果CI1
        /// </summary>
        public TestResult ResultCI1
        {
            get { return m_ResultCI1; }
            set
            {
                if (value == m_ResultCI1)
                {
                    return;
                }
                m_ResultCI1 = value;
                RaisePropertyChanged(() => ResultCI1);
            }
        }
        /// <summary>
        /// 测试结果CI2
        /// </summary>
        public TestResult ResultCI2
        {
            get { return m_ResultCI2; }
            set
            {
                if (value == m_ResultCI2)
                {
                    return;
                }
                m_ResultCI2 = value;
                RaisePropertyChanged(() => ResultCI2);
            }
        }
        /// <summary>
        /// 测试结果CI3
        /// </summary>
        public TestResult ResultCI3
        {
            get { return m_ResultCI3; }
            set
            {
                if (value == m_ResultCI3)
                {
                    return;
                }
                m_ResultCI3 = value;
                RaisePropertyChanged(() => ResultCI3);
            }
        }
        /// <summary>
        /// 测试结果CI4
        /// </summary>
        public TestResult ResultCI4
        {
            get { return m_ResultCI4; }
            set
            {
                if (value == m_ResultCI4)
                {
                    return;
                }
                m_ResultCI4 = value;
                RaisePropertyChanged(() => ResultCI4);
            }
        }
        /// <summary>
        /// 测试结果CI5
        /// </summary>
        public TestResult ResultCI5
        {
            get { return m_ResultCI5; }
            set
            {
                if (value == m_ResultCI5)
                {
                    return;
                }
                m_ResultCI5 = value;
                RaisePropertyChanged(() => ResultCI5);
            }
        }
        /// <summary>
        /// 测试结果CI6
        /// </summary>
        public TestResult ResultCI6
        {
            get { return m_ResultCI6; }
            set
            {
                if (value == m_ResultCI6)
                {
                    return;
                }
                m_ResultCI6 = value;
                RaisePropertyChanged(() => ResultCI6);
            }
        }

        public override void RestartTest()
        {
            ResultCI1 = TestResult.Unkown;
            ResultCI2 = TestResult.Unkown;
            ResultCI3 = TestResult.Unkown;
            ResultCI4 = TestResult.Unkown;
            ResultCI5 = TestResult.Unkown;
            ResultCI6 = TestResult.Unkown;

            CuurentCI1 = "";
            CuurentCI2 = "";
            CuurentCI3 = "";
            CuurentCI4 = "";
            CuurentCI5 = "";
            CuurentCI6 = "";
        }
    }
}