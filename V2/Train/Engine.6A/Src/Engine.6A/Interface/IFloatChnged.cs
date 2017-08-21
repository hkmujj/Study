using MMI.Facility.Interface.Data;

namespace Engine._6A.Interface
{
    public interface IFloatChnged
    {
        void Changed(CommunicationDataChangedArgs<float> args);
    }
}