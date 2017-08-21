using System;
using MMI.Facility.Interface.Data;
using Tram.CBTC.Infrasturcture.Service;

namespace Tram.CBTC.Infrasturcture.Events
{
    public class CommunicationDataChangedWrapperArgs<T>
    {
        public CommunicationDataChangedWrapperArgs(CommunicationDataChangedArgs<T> changedArgs, global::Tram.CBTC.Infrasturcture.Model.CBTC cbtc,
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
        public global::Tram.CBTC.Infrasturcture.Model.CBTC CBTC { get; private set; }

        /// <summary>
        /// 接口适配服务 
        /// </summary>
        public IInterfaceAdapterService InterfaceAdapterService { get; private set; }

   
    }
}