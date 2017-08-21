using MMI.Facility.Interface.Data;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public interface IUpdateStatusProvider 
    {
        void UpdateState(object sender, CommunicationDataChangedArgs e);
    }
}