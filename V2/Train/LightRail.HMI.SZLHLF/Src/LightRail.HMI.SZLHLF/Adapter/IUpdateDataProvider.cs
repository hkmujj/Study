using MMI.Facility.Interface.Data;

namespace LightRail.HMI.SZLHLF.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}