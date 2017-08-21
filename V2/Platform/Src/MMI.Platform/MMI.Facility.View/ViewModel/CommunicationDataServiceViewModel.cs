using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Service;

namespace MMI.Facility.View.ViewModel
{
    public class CommunicationDataServiceViewModel : NotificationObject
    {
        public CommunicationDataServiceViewModel(ICommunicationDataKey communicationDataKey, ICommunicationDataService communicationDataService)
        {
            CommunicationDataKey = communicationDataKey;
            InData = new CommunicationDataServiceItemViewModel(communicationDataService.WritableReadService);
            OutData =
                new CommunicationDataServiceItemViewModel(
                    (IWritableCommunicationDataReadService) communicationDataService.WriteService);
        }

        public ICommunicationDataKey CommunicationDataKey { private set; get; }

        public CommunicationDataServiceItemViewModel InData { get; private set; }
        public CommunicationDataServiceItemViewModel OutData { get; private set; }
    }
}