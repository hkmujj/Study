using MMI.Facility.Interface.Data;

namespace Subway.CBTC.Casco.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}