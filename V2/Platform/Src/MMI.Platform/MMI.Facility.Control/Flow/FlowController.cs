using System;
using System.Threading;
using System.Windows.Forms;
using MMI.Facility.Control.Service;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Flow;
using MMI.Facility.View.Views.Common;

namespace MMI.Facility.Control.Flow
{
    internal class FlowController : IFlowController
    {

        private readonly FlowControllerBuilder m_FlowControllerBuilder;

        // ReSharper disable once InconsistentNaming
        protected MainBaseForm m_MainForm;

        public event EventHandler<FlowInitializeCompletedEventArgs> FlowInitializeCompletedEvent;

        public event FormClosedEventHandler FormClosed;

        public event EventHandler Load;

        public FlowController(StartModel startModel)
        {
            switch (startModel)
            {
                case StartModel.Edit:
                    m_FlowControllerBuilder = new EditModelFlowBuilder();
                    break;
                case StartModel.PTT:
                    m_FlowControllerBuilder = new PTTModelFlowBuilder();
                    break;
                default:
                    m_FlowControllerBuilder = new NormalModelFlowBuilder();
                    break;
            }
        }

        public virtual void Run()
        {
            InitalizeApp();

            InitalizeService();

            LoadConfig();

            m_MainForm = CreateForm();

            m_MainForm.FormClosed += OnFormClosed;
            m_MainForm.Load += OnLoad;

            LoadAddIns();

            InitalizeNet();

            InitalizeIndexDescriptionService();

            ThreadPool.QueueUserWorkItem(state =>
            {
                LoadDefaultIndexDescription();
            });

            InitalizeRunningParam();

            InitalizeMainForm();

            InitalizeObjects();

            InitalizeSubsystem();

            InitalizeHook();

            ThreadPool.QueueUserWorkItem(s => StartNet());

            FinalInitalize();

            Application.Run(m_MainForm);
        }

        private void StartNet()
        {
            m_FlowControllerBuilder.StartNet();
        }

        private void LoadDefaultIndexDescription()
        {
            m_FlowControllerBuilder.LoadDefaultIndexDescription();
        }

        private void InitalizeIndexDescriptionService()
        {
            m_FlowControllerBuilder.InitalizeIndexDescriptionService();
        }

        private void InitalizeSubsystem()
        {
            m_FlowControllerBuilder.InitalizeSubsystem();
        }

        private void InitalizeService()
        {
            m_FlowControllerBuilder.InitalizeService();
        }

        private void InitalizeApp()
        {
            App.Current.ServiceManager = ServiceManager.Instance;
        }

        private void InitalizeRunningParam()
        {
            m_FlowControllerBuilder.InitalizeRunningParam();
        }


        /// <summary>
        /// 初始化对象
        /// </summary>
        private void InitalizeObjects()
        {
            m_FlowControllerBuilder.InitalizeObjects();
        }

        private void InitalizeMainForm()
        {
            m_FlowControllerBuilder.InitalizeMainForm(m_MainForm);
        }

        private void LoadAddIns()
        {
            m_FlowControllerBuilder.LoadAddIns();
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig()
        {
            m_FlowControllerBuilder.LoadConfig();
        }

        /// <summary>
        /// 初始化网络
        /// </summary>
        private void InitalizeNet()
        {
            m_FlowControllerBuilder.InitalizeNet();
            m_FlowControllerBuilder.RegistNet();
        }

        /// <summary>
        /// 初始化键盘钩子
        /// </summary>
        private void InitalizeHook()
        {
            m_FlowControllerBuilder.InitalizeHook();
        }


        private MainBaseForm CreateForm()
        {
            return m_FlowControllerBuilder.CreateForm();
        }

        private void FinalInitalize()
        {
            m_FlowControllerBuilder.FinalInitalize();
        }


        protected void OnFlowInitializeCompletedEvent(object sender, FlowInitializeCompletedEventArgs e)
        {
            var handler = FlowInitializeCompletedEvent;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            var handler = FormClosed;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void OnLoad(object sender, EventArgs e)
        {
            var handler = Load;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
