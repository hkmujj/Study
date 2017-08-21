using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Urban.Domain.TrainState.Interface
{
    public interface IDataListener
    {
        void VoluntaryResponse(ICommunicationDataService dataService);

        void OnDataChanged(object sender, CommunicationDataChangedArgs communicationDataChangedArgs);
    }
}