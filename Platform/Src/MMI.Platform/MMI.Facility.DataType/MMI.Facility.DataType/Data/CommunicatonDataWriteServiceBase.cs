using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.DataType.Data
{
    public class CommunicatonDataWriteServiceBase : CommunicatonDataServiceBase
    {
        
        protected override void InitChangedDataBuffer(INetDataConfig netDataConfig)
        {
            m_ChangedDataBuffer =
                new CommunicationChangedDataBuffer(netDataConfig.NetOutputDataPackage.GetTotalBoolCount(),
                    netDataConfig.NetOutputDataPackage.GetTotalFloatCount());
        }

        protected override void InitBoolDic(INetDataConfig config)
        {
            //BoolDic =
            //    new DictionaryProxy<int, bool>(
            //        Enumerable.Range(config.NetOutputDataPackage.BoolMappedStartIndex,
            //            config.NetOutputDataPackage.BoolMappedStartIndex +
            //            config.NetOutputDataPackage.GetTotalBoolCount())
            //            .Select(s => new IndexValueDescriptionModel<int, bool>() {Index = s})
            //            .ToList());
        }

        protected override void InitFloatDic(INetDataConfig config)
        {
            //FloatDic =

            //    new DictionaryProxy<int, float>(
            //        Enumerable.Range(config.NetOutputDataPackage.FloatMappedStartIndex,
            //            config.NetOutputDataPackage.FloatMappedStartIndex +
            //            config.NetOutputDataPackage.GetTotalFloatCount())
            //            .Select(s => new IndexValueDescriptionModel<int, float>() {Index = s})
            //            .ToList());
        }

        public override sealed void NetServiceOnDataReceived(NetDatas netDatas)
        {
            // nothing need to do
        }


        public CommunicatonDataWriteServiceBase(IConfig config, INetProjectDataConfig projectDataConfig, CommunicationDataEntity communicationDataEntity) : base(communicationDataEntity)
        {
        }
    }
}