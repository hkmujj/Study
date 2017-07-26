using MMI.Facility.Interface.Data;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model.Events
{
    public class CommunicationDataChangedWrapperArgs<T>
    {
        public CommunicationDataChangedWrapperArgs(CommunicationDataChangedArgs<T> changedArgs, IATP atp,
            IInterfaceAdapterService interfaceAdapterService)
        {
            ChangedDatas = changedArgs;
            ATP = atp;
            InterfaceAdapterService = interfaceAdapterService;
        }

        /// <summary>
        /// 变化的数据
        /// </summary>
        internal CommunicationDataChangedArgs<T> ChangedDatas { get; private set; }

        /// <summary>
        /// 关联的ATP
        /// </summary>
        public IATP ATP { get; private set; }

        /// <summary>
        /// 接口适配服务 
        /// </summary>
        public IInterfaceAdapterService InterfaceAdapterService { get; private set; }

    }
}