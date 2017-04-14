using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Data
{
    public class CommunicatonDataReadServiceBase : CommunicatonDataServiceBase
    {

        protected override void InitChangedDataBuffer(INetDataConfig netDataConfig)
        {
            m_ChangedDataBuffer =
                new CommunicationChangedDataBuffer(netDataConfig.NetInputDataPackage.GetTotalBoolCount(),
                    netDataConfig.NetInputDataPackage.GetTotalFloatCount());
        }

        protected override void InitBoolDic(INetDataConfig config)
        {
            //BoolDic =
            //    new DictionaryProxy<int, bool>(
            //        Enumerable.Range(0, config.NetInputDataPackage.GetTotalBoolCount())
            //            .Select(s => new IndexValueDescriptionModel<int, bool>() { Index = s })
            //            .ToList()) {  };
        }

        protected override void InitFloatDic(INetDataConfig config)
        {
            //FloatDic =
            //    new DictionaryProxy<int, float>(
            //        Enumerable.Range(0, config.NetInputDataPackage.GetTotalFloatCount())
            //            .Select(s => new IndexValueDescriptionModel<int, float>() { Index = s })
            //            .ToList()) {  };
        }

        protected CommunicatonDataReadServiceBase(IConfig config, INetProjectDataConfig projectDataConfig, CommunicationDataEntity communicationDataEntity) : base(communicationDataEntity)
        {
        }
    }
}