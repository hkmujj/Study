using MMI.Facility.Interface.Data;

namespace Subway.CBTC.THALES.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}