using System.Diagnostics;
using CBTC.Infrasturcture.Model.Constant;
using CBTC.Infrasturcture.Model.Fault;
using CBTC.Infrasturcture.Model.Hardware;
using CBTC.Infrasturcture.Model.Msg;
using CBTC.Infrasturcture.Model.Others;
using CBTC.Infrasturcture.Model.Road;
using CBTC.Infrasturcture.Model.Running;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.Model.Signal;
using CBTC.Infrasturcture.Model.Test;
using CBTC.Infrasturcture.Model.Train;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;

namespace CBTC.Infrasturcture.Model
{
    public class CBTC : NotificationObject
    {
        [DebuggerStepThrough]
        public CBTC()
        {
            TrainInfo = new TrainInfo();
            RunningInfo = new RunningInfo();
            SignalInfo = new SignalInfo();
            RoadInfo = new RoadInfo();
            Message = new Message();
            Other = new Other();
            FaultInfo = new FaultInfo();
            Hardware = new CBTCHardware();
            TestInfo = new TestInfo();
        }
        public virtual void Initalize()
        {

        }

        public ScreenIdentity Identity { get; protected set; }

        public IServiceManager ServiceManager { get; protected set; }

        public ICommunicationDataService DataService { get; protected set; }

        public SubsystemInitParam InitParam { protected set; get; }

        public SignalType Type { get; protected set; }

        public TrainInfo TrainInfo { get; protected set; }

        public RunningInfo RunningInfo { get; protected set; }

        public SignalInfo SignalInfo { get; protected set; }

        public RoadInfo RoadInfo { get; protected set; }

        public Message Message { get; protected set; }
        
        public FaultInfo FaultInfo { get; protected set; }

        public Other Other { get; protected set; }

        public CBTCHardware Hardware { get; protected set; }

        public TestInfo TestInfo { get; set; }

        public ISendInterface SendInterface { get; set; }
    }
}