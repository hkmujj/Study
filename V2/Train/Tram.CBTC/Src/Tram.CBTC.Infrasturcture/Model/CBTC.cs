using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface.Project;
using MMI.Facility.Interface.Service;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Fault;
using Tram.CBTC.Infrasturcture.Model.Hardware;
using Tram.CBTC.Infrasturcture.Model.Msg;
using Tram.CBTC.Infrasturcture.Model.Others;
using Tram.CBTC.Infrasturcture.Model.Recive;
using Tram.CBTC.Infrasturcture.Model.Road;
using Tram.CBTC.Infrasturcture.Model.Running;
using Tram.CBTC.Infrasturcture.Model.Send;
using Tram.CBTC.Infrasturcture.Model.Signal;
using Tram.CBTC.Infrasturcture.Model.Test;
using Tram.CBTC.Infrasturcture.Model.Train;

namespace Tram.CBTC.Infrasturcture.Model
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
            TimeTableInfo = new TimeTableInfo();
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
        public IRecive Recive { get; protected set; }

        public TimeTableInfo TimeTableInfo { get; protected set; }
    }
}