using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using CommonUtil.Util.Extension;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Running;
using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface.Project;
using MMI.Facility.View.Views.CommunicationData;

namespace MMI.Facility.View.Views.Shell
{
    public partial class FormalizeForm
    {
        public FormalizeForm()
        {
            InitializeComponent();
        }

        private void FormalizeForm_Load(object sender, System.EventArgs e)
        {
            var fApp = Config.AppConfigs.FirstOrDefault();

            if (fApp!= null)
            {
                var firstWindRect = fApp.ActureFormConfig.Rectangle;
                var centor = firstWindRect.Location.Translate(firstWindRect.Size.Width/2 - Size.Width/2,
                    firstWindRect.Size.Height/2 - Size.Height/2);
                Location = centor;
            }
        }

        /// <summary>
        /// ³ÌÐò¹Ø±Õ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormalizeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CommunicationDataService.Dispose();
        }

        public override IShareForm CreateShareForm()
        {
            if (Config.SystemConfig.IsDebugModel)
            {
                var rect = Config.DebugWindowConfig.FirstOrDefaultRectangle(typeof(CommunicationDataForm));
                var tmpSf = new CommunicationDataForm(DataPackage)
                            {
                                ShowInTaskbar = true,
                                Location = rect.Location,
                                Size = rect.Size,
                            };

                return tmpSf;
            }
            return null;
        }

        public override IEnumerable<IViewForm> CreateViewForms()
        {
            RunningViewService.Initalize(new ViewServiceInitalizeParam(null, DataPackage));
            foreach (var projectFormBase in RunningViewService.AllViewFormCollection)
            {
                var appConfig = Config.AppConfigs.FirstOrDefault(f => f.AppName == projectFormBase.AppName);
                if (appConfig == null)
                {
                    LogMgr.Error(string.Format((string) "Can not found app config of {0}. when set it's form topmost.", (object) projectFormBase.AppName));
                }
                else
                {
                    projectFormBase.TopMost = appConfig.ActureFormConfig.TopMost;
                }
                projectFormBase.DebugInfoVisible = Config.SystemConfig.IsDebugModel;

            }
            if (RunningViewService.ActivedFormCollection != null && Enumerable.Any<ProjectFormBase>(RunningViewService.ActivedFormCollection))
            {
                Location = RunningViewService.ActivedFormCollection[0].DesktopLocation;
            }
            return RunningViewService.ActivedFormCollection;
        }


    }
}