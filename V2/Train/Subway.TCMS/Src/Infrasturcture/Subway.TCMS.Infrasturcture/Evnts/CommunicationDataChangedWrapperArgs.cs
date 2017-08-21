using MMI.Facility.Interface.Data;
using Subway.TCMS.Infrasturcture.Service;

namespace Subway.TCMS.Infrasturcture.Evnts
{
    public class CommunicationDataChangedWrapperArgs<T>
    {
        public CommunicationDataChangedWrapperArgs(CommunicationDataChangedArgs<T> dataChangedArgs, IProjectDecriptionService projectDecriptionService)
        {
            DataChangedArgs = dataChangedArgs;
            ProjectDecriptionService = projectDecriptionService;
        }

        public CommunicationDataChangedArgs<T> DataChangedArgs { get; private set; }
        public IProjectDecriptionService ProjectDecriptionService { get; private set; }
    }
}
