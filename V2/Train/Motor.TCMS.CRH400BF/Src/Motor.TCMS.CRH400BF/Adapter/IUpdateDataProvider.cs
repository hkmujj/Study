using MMI.Facility.Interface.Data;

namespace Motor.TCMS.CRH400BF.Adapter
{
    public interface IUpdateDataProvider
    {
        void Initalize(bool isDebugModel);

        void UpdateDatas(object sender, CommunicationDataChangedArgs args);
    }
}