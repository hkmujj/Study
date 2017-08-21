using MMI.Facility.Interface.Data;

namespace Subway.ShenZhenLine11.Interface
{
    public interface IStatusChanged
    {
        void Changed(object sender, CommunicationDataChangedArgs<bool> args);
        void Changed(object sender, CommunicationDataChangedArgs<float> args);
    }
}