using MMI.Facility.Interface.Data;

namespace Engine._6A.Interface
{
    public interface IBoolDataChanged
    {
        void Changed(CommunicationDataChangedArgs<bool> args);
    }
}