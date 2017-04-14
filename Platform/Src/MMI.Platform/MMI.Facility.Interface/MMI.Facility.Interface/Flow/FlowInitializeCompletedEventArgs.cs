using System;
using System.Diagnostics;
using MMI.Facility.Interface.Data;

namespace MMI.Facility.Interface.Flow
{
    /// <summary>
    /// 
    /// </summary>
    public class FlowInitializeCompletedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPackage"></param>
        [DebuggerStepThrough]
        public FlowInitializeCompletedEventArgs(IDataPackage dataPackage)
        {
            DataPackage = dataPackage;
        }

        /// <summary>
        /// 
        /// </summary>
        public IDataPackage DataPackage { private set; get; }
    }
}