using System.Collections.Generic;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using YunDa.JC.MMI.Common;
using SH_Reconnect.Views;

namespace SH_Reconnect
{
    public partial class MainView : ProjectFormBase, IDataChangedListener
    {
        ViewManager viewManager;
        public MainView(SubsystemInitParam initParam, ICommunicationDataService dataService, ICourseService courseService)
        {
            var app = initParam.AppConfig;
            AppName = app.AppName;
            AppConfig = app;
            DataPackage = initParam.DataPackage;

            InitializeComponent();

            this.viewManager = new ViewManager(_viewParent);
            
            SH_RC_V1_EquipmentData v1 = new SH_RC_V1_EquipmentData(1, viewManager, dataService);
            SH_RC_V2_Net v2 = new SH_RC_V2_Net(2, viewManager, dataService);
            SH_RC_V3_Net v3 = new SH_RC_V3_Net(3, viewManager, dataService);
            SH_RC_V3_1_Net v31 = new SH_RC_V3_1_Net(31, viewManager, dataService);
            SH_RC_V3_2_Net v32= new SH_RC_V3_2_Net(32, viewManager, dataService);
            SH_RC_V3_3_Net v33 = new SH_RC_V3_3_Net(33, viewManager, dataService);
            SH_RC_V3_4_Net v34 = new SH_RC_V3_4_Net(34, viewManager, dataService);
            SH_RC_V4_Net v4 = new SH_RC_V4_Net(4, viewManager, dataService);
            SH_RC_V4_1_Net v41 = new SH_RC_V4_1_Net(41, viewManager, dataService);
            SH_RC_V5_Net v5 = new SH_RC_V5_Net(5, viewManager, dataService);
            SH_RC_V6_Net v6 = new SH_RC_V6_Net(6, viewManager, dataService);

            SH_RC_V0_BackView v0 = new SH_RC_V0_BackView(0, viewManager, dataService);

            DataChangedProxy.Instance.Regist(this);
            //dataService.ReadService.BoolChanged += onViewChanged;
            if (dataService.ReadService.ReadOnlyBoolDictionary[1300])
            {
                this.viewManager.CurrentViewID = 1;
            }
            else
            {
                this.viewManager.CurrentViewID = 0;
            }

        }
        //void onViewChanged(object sender, CommunicationDataChangedArgs<bool> e)
        //{
        //    foreach (var cmd in e.NewValue)
        //    {
        //        if (cmd.Key == 1318)
        //        {
        //            if (cmd.Value)
        //            {
        //                viewManager.CurrentViewID = 1;
        //            }
        //            else 
        //            {
        //                viewManager.CurrentViewID = 0;
        //            }
        //        }
        //    }
        //}

        public void OnBoolItemChanged(ref KeyValuePair<int, bool> item)
        {
            if (item.Key == 1318)
            {
                if (item.Value)
                {
                    viewManager.CurrentViewID = 1;
                }
                else
                {
                    viewManager.CurrentViewID = 0;
                }
            }
        }

        public void OnFloatItemChanged(ref KeyValuePair<int, float> item)
        {
            
        }
    }
}
