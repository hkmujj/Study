using MMI.Facility.Interface.Data;

namespace Engine.TCMS.Turkmenistan.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}