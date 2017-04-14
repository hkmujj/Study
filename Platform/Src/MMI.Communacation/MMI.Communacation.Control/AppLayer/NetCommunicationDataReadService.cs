using MMI.Communacation.Interface.AppLayer;
using MMI.Facility.DataType.Data;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Communacation.Control.AppLayer
{
    class NetCommunicationDataReadService : CommunicatonDataReadServiceBase
    {
        private object m_BoolLocker = new object();
        private object m_FloatLocker = new object();
        private object m_DataLocker = new object();

        public override void NetServiceOnDataReceived(NetDatas netDatas)
        {
            CommunicationDataChangedArgs<bool> changedBool;
            ChangeBools(netDatas.ReceivedBools.DataList, netDatas.ReceivedBools.StartIndex, out changedBool);

            CommunicationDataChangedArgs<float> changedFloat;
            ChangeFloats(netDatas.ReceivedFloats.DataList, netDatas.ReceivedFloats.StartIndex, out changedFloat);

            NotifyDataChanged(this, new CommunicationDataChangedArgs(changedBool, changedFloat));

        }

        public NetCommunicationDataReadService(IConfig config, INetProjectDataConfig projectDataConfig, CommunicationDataEntity communicationDataEntity)
            : base(config, projectDataConfig, communicationDataEntity)
        {
        }

        protected override void NotifyDataChanged(object sender, CommunicationDataChangedArgs e)
        {
            lock (m_DataLocker)
            {
                base.NotifyDataChanged(sender, e);
            }
        }

        protected override void NotifyBoolChanged(object sender, CommunicationDataChangedArgs<bool> e)
        {
            lock (m_BoolLocker)
            {
                base.NotifyBoolChanged(sender, e);
            }
        }

        protected override void NotifyFloatChanged(object sender, CommunicationDataChangedArgs<float> e)
        {
            lock (m_FloatLocker)
            {
                base.NotifyFloatChanged(sender, e);
            }
        }
    }
}
