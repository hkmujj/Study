using CBTC.Infrasturcture.Service;
using MMI.Facility.Interface.Data;

namespace CBTC.Infrasturcture.Events
{
    public class CommunicationDataChangedWrapperArgs<T>
    {
        public CommunicationDataChangedWrapperArgs(CommunicationDataChangedArgs<T> changedArgs, CBTC.Infrasturcture.Model.CBTC cbtc,
            IInterfaceAdapterService interfaceAdapterService)
        {
            ChangedDatas = changedArgs;
            CBTC = cbtc;
            InterfaceAdapterService = interfaceAdapterService;
        }

        /// <summary>
        /// 变化的数据
        /// </summary>
        internal CommunicationDataChangedArgs<T> ChangedDatas { get; private set; }

        /// <summary>
        /// 关联的ATP
        /// </summary>
        public CBTC.Infrasturcture.Model.CBTC CBTC { get; private set; }

        /// <summary>
        /// 接口适配服务 
        /// </summary>
        public IInterfaceAdapterService InterfaceAdapterService { get; private set; }

    }
}