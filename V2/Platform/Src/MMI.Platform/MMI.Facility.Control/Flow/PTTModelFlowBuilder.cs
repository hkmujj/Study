using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MMI.Communacation.Control.AppLayer;
using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Control.Command;
using MMI.Facility.Control.Data;
using MMI.Facility.Control.Service;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.Interface.Event;
using MMI.Facility.Interface.Project;
using MMI.Facility.View.Views;
using MMI.Facility.View.Views.Common;
using MMI.Facility.View.Views.Shell;

namespace MMI.Facility.Control.Flow
{
    class PTTModelFlowBuilder : FlowControllerBuilder
    {
        public override MainBaseForm CreateForm()
        {
            return new PTTMainFrom();
        }

        public override void InitalizeNet()
        {
            var service = new NetCommunicationDataFacadeService(ConfigManager.Instance.Config);
            m_CommunicatonDataFacadeService = service;
            m_CommunicatonDataFacadeService.NetServiceBegin += (sender, args) =>
            {
                var concret = (NetCommandEventArgs) args;
                var cmdInterpreter = CommandInterpreterFactory.GetContentInterpreter(concret.Command);
                SysLog.Info("There is not any avilable commander to interpreter start command !!!");
                if (cmdInterpreter != null)
                {
                    var cmdParam = cmdInterpreter.Interpreter(concret.Command);
                    SysLog.Info("Course startted, ther start param = {0}", cmdParam);
                    ((EventService) m_EventService).OnCoursStarting(
                        new CourseStartEventArgs(cmdParam));
                }
            };
            m_CommunicatonDataFacadeService.NetServiceEnd +=
                (sender, args) => ((EventService) m_EventService).OnCourseStopping();
            m_CommunicatonDataFacadeService.StationCollectionUpdated +=
                (sender, args) => m_StationNameProviderService.UpdateStaionDictionay(args.Stations);

            service.InitalizeDataServiceDictionary();

            RegistDataChangedEvents();
        }

        public override void InitalizeRunningParam()
        {
            m_RunningParamManager = new RunningParamManager(ConfigManager.Instance.Config, m_AddInLoader,
                m_CommunicatonDataFacadeService, StartModel.PTT, m_IndexDescriptionService);
            m_RunningParamManager.Initalize();
        }

        public override void InitalizeService()
        {
            base.InitalizeService();
            ((ViewService) m_ViewService).AllViewFormChanged += (service, args) =>
            {
                foreach (ProjectFormBase newItem in args.NewItems)
                {
                    SetPTTButtonMode(newItem);
                }
            };
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (sender.GetType().IsSubclassOf(typeof(ProjectFormBase)))
            {
                ((ProjectFormBase) sender).SetDesktopLocation(
                    ((ProjectFormBase) sender).AppConfig.ActureFormConfig.Rectangle.X,
                    ((ProjectFormBase) sender).AppConfig.ActureFormConfig.Rectangle.Y);
            }

            e.Cancel = true;
        }

        private void SetPTTButtonMode(ProjectFormBase setFrom)
        {
            setFrom.MinimizeBox = false;
            setFrom.MaximizeBox = false;
            setFrom.FormClosing += OnFormClosing;
        }
    }
}
