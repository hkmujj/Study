using MMI.Facility.Interface.Data;

namespace LightRail.HMI.SZLHLF.Interface
{
    public interface IDataChanged
    {
        void DataChanged(object sender, CommunicationDataChangedArgs<bool> args);
        void DataChanged(object sender, CommunicationDataChangedArgs<float> args);
    }
}
