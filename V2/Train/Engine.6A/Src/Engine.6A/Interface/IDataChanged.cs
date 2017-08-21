using MMI.Facility.Interface.Data;

namespace Engine._6A.Interface
{
    public interface IDataChanged : IFloatChnged, IBoolDataChanged
    {
        void DataChnged(object sender, CommunicationDataChangedArgs args);
    }
}