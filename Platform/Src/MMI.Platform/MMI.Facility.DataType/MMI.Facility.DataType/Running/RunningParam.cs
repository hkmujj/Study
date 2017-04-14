using System.Collections.Generic;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Running;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.DataType.Running
{
    public class RunningParam : IRunningParam
    {

        public RunningParam(StartModel startModel)
        {
            StartModel = startModel;
            AppRunningParamDictionary = new Dictionary<string, IAppRunningParam>();
        }

        public StartModel StartModel { get; private set; }


        public Dictionary<string, IAppRunningParam> AppRunningParamDictionary { get; private set; }

        /// <summary>
        /// 通信数据
        /// </summary>
        public ICommunicationDataFacadeService CommunicationFacadeDataService { get; set; }

        //public IIndexInterpreter IndexInterpreter { get; set; }
    }
}
