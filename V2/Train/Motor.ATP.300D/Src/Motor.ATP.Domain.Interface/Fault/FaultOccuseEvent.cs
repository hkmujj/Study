//using System.Collections.ObjectModel;
//using System.Diagnostics;


//namespace Motor.ATP.Domain.Interface.Fault
//{
//    public class FaultOccuseEvent : CompositePresentationEvent<FaultOccuseEventArgs>
//    {
//    }

//    public class FaultOccuseEventArgs
//    {
//        [DebuggerStepThrough]
//        public FaultOccuseEventArgs(IFaultItem faultItem, ReadOnlyCollection<ScreenIdentity> targetATPCollection)
//        {
//            TargetATPCollection = targetATPCollection;
//            FaultItem = faultItem;
//        }

//        /// <summary>
//        /// 目标
//        /// </summary>
//        public ReadOnlyCollection<ScreenIdentity> TargetATPCollection { private set; get; }

//        public IFaultItem FaultItem { private set; get; }
//    }
//}
