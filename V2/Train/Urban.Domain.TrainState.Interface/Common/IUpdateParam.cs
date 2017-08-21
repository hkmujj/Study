using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Urban.Domain.TrainState.Interface.Common
{
    public interface IUpdateParam
    {
        ICommunicationDataService DataService { get; }

        CommunicationIndexFacade IndexFacade { get; }

        CommunicationDataChangedArgs<bool> ChangedBools { get; }

        CommunicationDataChangedArgs<float> ChangedFloats { get; }

        baseClass BaseClass { get; }
    }

}