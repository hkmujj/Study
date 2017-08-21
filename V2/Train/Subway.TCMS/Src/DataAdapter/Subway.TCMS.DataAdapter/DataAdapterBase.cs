using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Subway.TCMS.Infrasturcture.Evnts;
using Subway.TCMS.Infrasturcture.Service;

namespace Subway.TCMS.DataAdapter
{
    public abstract class DataAdapterBase : IDataListener
    {
        protected Infrasturcture.Model.TCMS TCMS { get; set; }
        private IProjectDecriptionService m_ProjectDecriptionService;
        protected IProjectDecriptionService ProjectDecriptionService
        {
            get
            {
                return m_ProjectDecriptionService ?? (m_ProjectDecriptionService =
                           App.Current.ServiceManager.GetService<IProjectDecriptionService>(TCMS.Type.ToString()));
            }
        }
        protected DataAdapterBase(Infrasturcture.Model.TCMS tcms)
        {
            TCMS = tcms;
        }

        /// <summary>bool 值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            OnBoolChangedImp(sender, new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs, ProjectDecriptionService));
        }

        /// <summary>float值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            OnFloatChangedImp(sender, new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs, ProjectDecriptionService));
        }

        /// <summary>data值变化</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            OnDataChangedImp(sender, new CommunicationDataChangedWrapperArgs<bool>(dataChangedArgs.ChangedBools, ProjectDecriptionService), new CommunicationDataChangedWrapperArgs<float>(dataChangedArgs.ChangedFloats, ProjectDecriptionService));
        }

        public virtual void OnBoolChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> args)
        {

        }
        public virtual void OnFloatChangedImp(object sender, CommunicationDataChangedWrapperArgs<float> args)
        {

        }

        public virtual void OnDataChangedImp(object sender, CommunicationDataChangedWrapperArgs<bool> boolArgs,
            CommunicationDataChangedWrapperArgs<float> floatArgs)
        {

        }
        /// <summary>
        /// 课程结束时清除数据
        /// </summary>
        public abstract void ClearDataOnCourceStop();
        /// <summary>
        /// 清除数据
        /// </summary>
        protected virtual void ClearData() { }
    }
}
