using MMI.Facility.Interface.Data;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public interface IChangeStatus
    {
        void ChangeBools(CommunicationDataChangedArgs<bool> args);
        void ChangeFloats(CommunicationDataChangedArgs<float> args);
    }
}