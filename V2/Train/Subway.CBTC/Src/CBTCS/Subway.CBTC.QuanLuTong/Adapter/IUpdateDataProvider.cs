using MMI.Facility.Interface.Data;

namespace Subway.CBTC.QuanLuTong.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}