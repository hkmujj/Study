using System;
using System.Runtime.InteropServices.ComTypes;
using CommonUtil.Util;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Motor.LKJ._2000.DataAdapter.Resource;
using Motor.LKJ._2000.DataAdapter.States;
using Motor.LKJ._2000.Entity.Model;
using Motor.LKJ._2000.Entity.ViewModel;

namespace Motor.LKJ._2000.DataAdapter
{
    public class LKJ2000DataAdapter : IDataListener
    {
        private readonly SubsystemInitParam m_InitParam;

        public LKJ2000MainViewModel ViewModel { private set; get; }

        protected IProjectIndexDescriptionConfig IndexDescription { private set; get; }

        protected ICommunicationDataService CommunicationDataService
        {
            get { return m_InitParam.CommunicationDataService; }
        }

        public LKJ2000DataAdapter(SubsystemInitParam initParam, LKJ2000MainViewModel viewModel)
        {
            m_InitParam = initParam;
            ViewModel = viewModel;
            IndexDescription =
                initParam.DataPackage.Config.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                    new CommunicationDataKey(ViewModel.SubsystemInitParam.AppConfig));

            initParam.RegistDataListener(this);

            SetValuesWhenDebug();
        }

        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            var ps = GetInBoolAt(InBoolKeys.LKJ电源状态) ? LKJPowerState.On : LKJPowerState.Off;

            var atps = (ATPState)(Convert.ToInt32(GetInFloatAt(InFloatKeys.ATP控车状态)));

            switch (ps)
            {
                case LKJPowerState.Off: 
                    ViewModel.Model.State = LKJState.Shutdown;
                    break;
                case LKJPowerState.On:
                    switch (atps)
                    {
                        case ATPState.CTCS0:
                            ViewModel.Model.State = LKJState.RunInLKJ;
                            break;
                        case ATPState.CTCS2:
                            ViewModel.Model.State = LKJState.RunInATP;
                            break;
                        default:
                            LogMgr.Warn("Can not parser ATP state where value={0}, force let current state = {1}", atps, LKJState.RunInATP.ToString());
                            ViewModel.Model.State = LKJState.RunInATP;
                            break;
                    }
                    break;
                default:
                    ViewModel.Model.State = LKJState.Shutdown;
                    LogMgr.Warn("Can not parser LKJ power state where value={0}, force let curret state = {1}", ps, LKJState.Shutdown);
                    break;
            }
        }

        private float GetInFloatAt(string key)
        {
            return CommunicationDataService.ReadService.GetFloatAt(
                IndexDescription.InFloatDescriptionDictionary[key]);
        }

        private bool GetInBoolAt(string key)
        {
            return CommunicationDataService.ReadService.GetBoolAt(
                IndexDescription.InBoolDescriptionDictionary[key]);
        }

        internal void SetValuesWhenDebug()
        {
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceOnDataChanged(sender, dataChangedArgs);
        }
    }
}
