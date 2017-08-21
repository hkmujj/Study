using MMI.Facility.Interface.Data;

namespace Subway.XiaMenLine1.Interface
{
    public interface IUpdateStatusProvider 
    {
        void UpdateState(object sender, CommunicationDataChangedArgs e);
    }
}