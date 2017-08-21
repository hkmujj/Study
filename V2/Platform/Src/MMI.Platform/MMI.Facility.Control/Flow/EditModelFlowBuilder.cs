using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using MMI.Facility.Control.Data;
using MMI.Facility.Control.Service;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config.Net;
using MMI.Facility.Interface.Data.Config.NetDataPackage;
using MMI.Facility.View.Views;
using MMI.Facility.View.Views.Common;
using MMI.Facility.View.Views.Shell;

namespace MMI.Facility.Control.Flow
{
    class EditModelFlowBuilder : FlowControllerBuilder
    {
        public override MainBaseForm CreateForm()
        {
            var form = new ObjectEditUI();

            return form;
        }

        public override void InitalizeService()
        {
            base.InitalizeService();

            m_DebugViewService.DebugFormCollection.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (Form item in args.NewItems)
                        {
                            ListenRegionChanged(item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        // TODO delete listener
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }

        private void ListenRegionChanged(Form ui)
        {
            if (ui == null)
            {
                return;
            }

            ui.LocationChanged += (o, eventArgs) =>
            {
                var target = (Form)o;
                ConfigManager.Instance.SaveDebugWindRunningConfig(ui, target.Bounds);
            };
            ui.SizeChanged += (o, eventArgs) =>
            {
                var target = (Form)o;
                ConfigManager.Instance.SaveDebugWindRunningConfig(ui, target.Bounds);
            };
        }

        public override void InitalizeNet()
        {
            var service = new EditCommunicationDataFacadeService(ConfigManager.Instance.Config);

            service.InitalizeDataServiceDictionary();

            m_CommunicatonDataFacadeService = service;

            RegistDataChangedEvents();
        }

        public override void InitalizeRunningParam()
        {
            m_RunningParamManager = new RunningParamManager(ConfigManager.Instance.Config, m_AddInLoader, m_CommunicatonDataFacadeService, StartModel.Edit, m_IndexDescriptionService);
            m_RunningParamManager.Initalize();
        }
    }
}
