using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MMI.Facility.DataType.Extension;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Running;
using MMI.Facility.PublicUI.Interface;
using MMI.Facility.View.Views.Common;

namespace MMI.Facility.View.Views.DebugViews
{
    public partial class MMILogicForm : Form, ILogicForm
    {
        public IDataPackage DataPackage { get; private set; }

        public bool IsMdiMode { get; set; }

        public void SetMdiParent(Form parentForm)
        {
            MdiParent = parentForm;
            IsMdiMode = true;
        }

        public void CloseForm()
        {
            Close();
        }

        public FormWindowState GetFormState()
        {
            return WindowState;
        }

        public void SetFormState(FormWindowState nFws)
        {
            WindowState = nFws;
        }

        public void ReastData()
        {
        }

        public void ShowForm()
        {
            Show();
        }

        public void ShowFormDialog()
        {
            ShowDialog();
        }

        public IConfig Config { private set; get; }

        public IRunningParam RunningParam { private set; get; }

        public MMILogicForm(IDataPackage dataPackage)
            : this()
        {
            DataPackage = dataPackage;
            Config = dataPackage.Config;
            RunningParam = dataPackage.RunningParam;

            ShowInTaskbar = false;

            InitializeLogicView();
        }

        public MMILogicForm()
        {
            InitializeComponent();
            IsMdiMode = false;
            
        }

        private Dictionary<string, LogicControl> m_LogicControls;

        public LogicControl CurrentSelectLogicControl
        {
            get { return m_LogicControls[tabCtlLogicView.SelectedTab.Text]; }
        }

        private void InitializeLogicView()
        {
            m_LogicControls = new Dictionary<string, LogicControl>();

            foreach (var appConfig in DataPackage.Config.AppConfigs)
            {
                tabCtlLogicView.TabPages.Add(appConfig.AppName, appConfig.AppName);

                var page = tabCtlLogicView.TabPages[appConfig.AppName];
                var ctol = new LogicControl(appConfig.AppLogicConfig);
                page.Controls.Add(ctol);
                m_LogicControls.Add(appConfig.AppName, ctol);
            }
        }

        private void MMILogicForm_Load(object sender, EventArgs e)
        {
            var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(MMILogicForm));
            Left = rect.Left;
            Top = rect.Top;
        }
    }
}
