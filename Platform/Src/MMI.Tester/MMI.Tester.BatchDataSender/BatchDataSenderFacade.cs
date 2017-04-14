using System;
using System.Windows.Forms;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using MMI.Tester.BatchDataSender.Model;
using MMI.Tester.BatchDataSender.View;

namespace MMI.Tester.BatchDataSender
{
    public class BatchDataSenderFacade
    {
        public void AttachSenderView(BatchDataSenderInitParam initParam)
        {
            BatchSenderParam.Instance.Initalize(initParam);

            var dbv = initParam.SubsystemParam.DataPackage.ServiceManager.GetService<IDebugViewService>();

            dbv.DebugFormCollection.Add(new BatchDataSenderForm()
            {
                Text = String.Format("{0} - {1}", "Batch data sender", initParam.SubsystemParam.AppConfig.AppName)
            });
        }
    }

    public class BatchDataSenderInitParam
    {
        public BatchDataSenderInitParam(SubsystemInitParam subsystemParam, string boolFile, string floatFile, Form parentForm)
        {
            SubsystemParam = subsystemParam;
            BoolFile = boolFile;
            FloatFile = floatFile;
            ParentForm = parentForm;
        }

        public SubsystemInitParam SubsystemParam { get; private set; }

        public string BoolFile { private set; get; }

        public string FloatFile { get; private set; }

        public Form ParentForm { get; private set; }
    }
}