using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Running;
using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Hook;
using MMI.Facility.Interface.Project;
using MMI.Facility.PublicUI.Interface;
using MMI.Facility.View.Views.Common;
using MMI.Facility.View.Views.CommunicationData;
using MMI.Facility.View.Views.DebugViews;

namespace MMI.Facility.View.Views.Shell
{
    public partial class ObjectEditUI : MainBaseForm
    {
        public override void HotKeyEvent(HotKeys eHotKeyValue)
        {
            if (eHotKeyValue == HotKeys.AltDown)
            {
                //显示所有界面
                ShowAllWindows();
            }
            base.HotKeyEvent(eHotKeyValue);
        }

        public ObjectEditUI()
        {
            InitializeComponent();
        }

        public override IShareForm CreateShareForm()
        {
            if (Config.SystemConfig.IsDebugModel)
            {
                var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(CommunicationDataForm));
                var tmpSf = new CommunicationDataForm(DataPackage)
                {
                    MdiParent = this,
                    Location = rect.Location,
                    Size = rect.Size,
                };

                return tmpSf;
            }

            return null;
        }

        public override ILogicForm CreateLogicForm()
        {
            if (Config.SystemConfig.IsDebugModel &&
                Config.AppConfigs.SelectMany(s => s.AppLogicConfig.AppLogicConfigDic).Any())
            {
                var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(MMILogicForm));
                var logicForm = new MMILogicForm(DataPackage)
                {
                    WindowState = FormWindowState.Normal,
                    MdiParent = this,
                    Location = rect.Location,
                    Size = rect.Size,
                };

                return logicForm;
            }

            return null;
        }

        public override IAttributeForm CreateAttributeForm()
        {
            if (Config.SystemConfig.IsDebugModel)
            {
                var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(ObjectAttribForm));
                var objectAttribForm = new ObjectAttribForm(DataPackage)
                {
                    WindowState = FormWindowState.Normal,
                    MdiParent = this,
                    TopMost = true,
                    Location = rect.Location,
                    Size = rect.Size,
                };


                objectAttribForm.FloatObjectattributeFormDoubleClick += index => ShareForm.SelectFloat(index);
                objectAttribForm.BoolObjectAttribeFormDoubelClick += index => ShareForm.SelectBool(index);
                return objectAttribForm;
            }

            return null;
        }

        public override IProjectListInfoForm CreateProjectListInfoForm()
        {
            if (Config.SystemConfig.IsDebugModel)
            {
                var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(ProjectListForm));
                var projectListForm = new ProjectListForm(DataPackage)
                {
                    MdiParent = this,
                    Location = rect.Location,
                    Size = rect.Size,
                };

                projectListForm.ObjectSelectChanged += PorjectViewFormObjectSelectChanged;

                return projectListForm;
            }

            return null;
        }

        public override IEnumerable<IViewForm> CreateViewForms()
        {
            RunningViewService.Initalize(new ViewServiceInitalizeParam(this, DataPackage));
            foreach (var formBase in RunningViewService.AllViewFormCollection)
            {
                formBase.DebugInfoVisible = Config.SystemConfig.IsDebugModel;
            }

            return RunningViewService.ActivedFormCollection;
        }


        private void PorjectViewFormObjectSelectChanged(string appName, IUIObject obj)
        {
            AttributeForm.Select(appName, obj.ObjectKey);
        }
    }
}
