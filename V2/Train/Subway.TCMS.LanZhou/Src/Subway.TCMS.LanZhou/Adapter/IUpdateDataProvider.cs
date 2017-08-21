using MMI.Facility.Interface.Data;

namespace Subway.TCMS.LanZhou.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}