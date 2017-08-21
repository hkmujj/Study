using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH380D.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}