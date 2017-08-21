using MMI.Facility.Interface.Data;

namespace Urban.Philippine.Adapter.Interface
{
    public interface IDataChanged
    {
        void DataChanged(object sender, CommunicationDataChangedArgs args);

        void DataChanged(CommunicationDataChangedArgs<bool> args);

        void DataChanged(CommunicationDataChangedArgs<float> args);
    }
}