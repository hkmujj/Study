using Mmi.Communication.Index.Adapter;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Model.Common
{
    public class UpdateParam : IUpdateParam
    {
        public ICommunicationDataService DataService { get; set; }
        public CommunicationIndexFacade IndexFacade { get; set; }
        public CommunicationDataChangedArgs<bool> ChangedBools { get; set; }
        public CommunicationDataChangedArgs<float> ChangedFloats { get; set; }
        public baseClass BaseClass { get; set; }
    }
}