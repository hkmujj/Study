using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Controller;
using Engine.TCMS.HXD3.Model.Interface;
using Engine.TCMS.HXD3.Model.TCMS.Domain.Constant;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.ViewModel.Domain
{
    [Export(typeof(IResetSupport))]
    [Export]
    public class MasterDriverControlViewModel : TestViewModelBase
    {
        private TestResult m_Result1;
        private TestResult m_Result2;

        [ImportingConstructor]
        public MasterDriverControlViewModel(TestController controller)
        {
            Controller = controller;
            controller.ViewModel = this;
            StateKeyName = StateKeys.Root_����״̬_����_��˾����;
        }

        /// <summary>
        /// �����ֱ���λȷ�Ͻ��
        /// </summary>
        public TestResult Result1
        {
            get { return m_Result1; }
            set
            {
                if (value == m_Result1)
                {
                    return;
                }

                m_Result1 = value;
                RaisePropertyChanged(() => Result1);
            }
        }

        /// <summary>
        /// �ƶ�1���ֱ�λȷ�Ͻ��
        /// </summary>
        public TestResult Result2
        {
            get { return m_Result2; }
            set
            {
                if (value == m_Result2)
                {
                    return;
                }

                m_Result2 = value;
                RaisePropertyChanged(() => Result2);
            }
        }

        public override void RestartTest()
        {
            Result1 = TestResult.Unkown;
            Result2 = TestResult.Unkown;
        }
    }
}