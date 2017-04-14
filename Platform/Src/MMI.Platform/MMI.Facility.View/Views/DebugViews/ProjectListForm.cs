using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using MMI.Facility.DataType.Extension;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.PublicUI.Interface;

namespace MMI.Facility.View.Views.DebugViews
{
    public partial class ProjectListForm : System.Windows.Forms.Form, IProjectListInfoForm
    {
        public ProjectListForm(IDataPackage dataPackage)
        {
            DataPackage = dataPackage;
            Config = dataPackage.Config;

            InitializeComponent();

            ShowInTaskbar = false;

            m_ProjectInfoForms = new List<IProjectInfoControl>();

            this.Load += (sender, args) =>
            {
                var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(ProjectListForm));
                Top = rect.Top;
                Left = rect.Left;
                InitProjectInfos();
                InitalizeChildren();
            };
        }

        protected IConfig Config { private set; get; }

        public event Action<string, IUIObject> ObjectSelectChanged;

        public event Action<string, bool> InputObjectIsOn;

        public IDataPackage DataPackage { get; private set; }

        public ReadOnlyCollection<IProjectInfoControl> ProjectInfoForms { get { return m_ProjectInfoForms.AsReadOnly(); } }

        private readonly List<IProjectInfoControl> m_ProjectInfoForms;

        public void InitalizeChildren()
        {
            m_ProjectInfoForms.ForEach(e =>
            {
                e.InitPorjectList();
                e.InitViewsList();
                e.InitClassList();
                e.InitObjectList();
            });
        }

        public void ReastData()
        {

        }

        private void InitProjectInfos()
        {
            foreach (var appConfig in DataPackage.Config.AppConfigs.Where(w => w.SubsystemType == SubsystemType.Addin))
            {
                var child = new PorjectViewControl(appConfig.AppName, DataPackage) { AutoSize = true, Dock = DockStyle.Fill };
                m_ProjectInfoForms.Add(child);
                tabControlProjcetInfo.TabPages.Add(appConfig.AppName, appConfig.AppName);
                var page = tabControlProjcetInfo.TabPages[appConfig.AppName];
                page.Controls.Add(child);
                page.SizeChanged += (o, args) =>
                {
                    child.Size = page.Size;
                };

                child.ObjectSelectChanged += o => ActionUtil.OnAction(ObjectSelectChanged, child.AppName, o);

                child.InputObjectIsOn += b => ActionUtil.OnAction(InputObjectIsOn, child.AppName, b);
            }
        }
    }
}
