using MMI.Facility.Interface.Data;

namespace Tram.CBTC.Casco.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}