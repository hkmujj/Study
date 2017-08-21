using MMI.Facility.Interface.Data;

namespace Engine.TPX21F.HXN5B.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}