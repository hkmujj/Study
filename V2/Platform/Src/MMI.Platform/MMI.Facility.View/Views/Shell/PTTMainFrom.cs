using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Util;
using MMI.Facility.DataType.Extension;
using MMI.Facility.DataType.Log;
using MMI.Facility.DataType.Running;
using MMI.Facility.DataType.View.Form;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using MMI.Facility.View.Views.Common;
using MMI.Facility.View.Views.CommunicationData;

namespace MMI.Facility.View.Views.Shell
{
    public partial class PTTMainFrom 
    {
        public PTTMainFrom()
        {
            InitializeComponent();
            FormClosed += OnFormClosed;
            Width = 0;
            Height = 0;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs formClosedEventArgs)
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
                    LogMgr.Error(string.Format("Can not found app config of {0}. when set it's form topmost.", projectFormBase.AppName));
                }
                else
                {
                    projectFormBase.TopMost = appConfig.ActureFormConfig.TopMost;
                }
                projectFormBase.DebugInfoVisible = false;
                SetFormHandel(projectFormBase, Config.SystemConfig.SubsystemConfigCollection.FirstOrDefault(f => f.Name == projectFormBase.AppName));

            }
            return RunningViewService.ActivedFormCollection;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Hide();
            ShowInTaskbar = false;
        }

        private void SetFormHandel(ProjectFormBase form, ISubsystemConfig config)
        {
            if (string.IsNullOrEmpty(config.ShareName))
            {
                return;
            }

            try
            {
                using (var mf = MemoryMappedFile.OpenExisting(config.ShareName))
                {
                    using (var cva = mf.CreateViewAccessor(config.ShareIndex * 4, 4))
                    {
                        cva.Write(0, form.Handle.ToInt32());
                    }
                }
            }
            catch (Exception e)
            {
                SysLog.Error("Write windown handle error . {0}", e);
            }
        }
    }
}
