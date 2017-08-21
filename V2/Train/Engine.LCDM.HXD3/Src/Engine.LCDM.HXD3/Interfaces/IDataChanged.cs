using MMI.Facility.Interface.Data;

namespace Engine.LCDM.HXD3.Interfaces
{
    public interface IDataChanged
    {
        void DataChanged(object sender, CommunicationDataChangedArgs<bool> args);
        void DataChanged(object sender, CommunicationDataChangedArgs<float> args);
    }
}